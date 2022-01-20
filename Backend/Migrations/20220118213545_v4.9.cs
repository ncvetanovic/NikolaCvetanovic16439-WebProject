using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v49 : Migration
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
                name: "LekRecept",
                columns: table => new
                {
                    LekoviID = table.Column<int>(type: "int", nullable: false),
                    ReceptiID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekRecept", x => new { x.LekoviID, x.ReceptiID });
                    table.ForeignKey(
                        name: "FK_LekRecept_Lek_LekoviID",
                        column: x => x.LekoviID,
                        principalTable: "Lek",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LekRecept_Recept_ReceptiID",
                        column: x => x.ReceptiID,
                        principalTable: "Recept",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LekRecept_ReceptiID",
                table: "LekRecept",
                column: "ReceptiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LekRecept");

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
