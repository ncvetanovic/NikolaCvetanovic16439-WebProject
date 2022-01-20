using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KlijentID",
                table: "Recept",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recept_KlijentID",
                table: "Recept",
                column: "KlijentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept",
                column: "KlijentID",
                principalTable: "Klijent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropIndex(
                name: "IX_Recept_KlijentID",
                table: "Recept");

            migrationBuilder.DropColumn(
                name: "KlijentID",
                table: "Recept");
        }
    }
}
