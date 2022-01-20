using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek");

            migrationBuilder.DropIndex(
                name: "IX_Lek_ReceptID",
                table: "Lek");

            migrationBuilder.DropColumn(
                name: "ReceptID",
                table: "Lek");

            migrationBuilder.CreateTable(
                name: "LekUReceptu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceptID = table.Column<int>(type: "int", nullable: false),
                    LekID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekUReceptu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LekUReceptu_Lek_LekID",
                        column: x => x.LekID,
                        principalTable: "Lek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LekUReceptu_Recept_ReceptID",
                        column: x => x.ReceptID,
                        principalTable: "Recept",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LekUReceptu_LekID",
                table: "LekUReceptu",
                column: "LekID");

            migrationBuilder.CreateIndex(
                name: "IX_LekUReceptu_ReceptID",
                table: "LekUReceptu",
                column: "ReceptID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LekUReceptu");

            migrationBuilder.AddColumn<int>(
                name: "ReceptID",
                table: "Lek",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lek_ReceptID",
                table: "Lek",
                column: "ReceptID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek",
                column: "ReceptID",
                principalTable: "Recept",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
