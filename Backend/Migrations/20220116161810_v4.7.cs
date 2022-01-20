using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v47 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recept_Apoteka_ApotekaID",
                table: "Recept");

            migrationBuilder.DropIndex(
                name: "IX_Recept_ApotekaID",
                table: "Recept");

            migrationBuilder.DropColumn(
                name: "ApotekaID",
                table: "Recept");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApotekaID",
                table: "Recept",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recept_ApotekaID",
                table: "Recept",
                column: "ApotekaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recept_Apoteka_ApotekaID",
                table: "Recept",
                column: "ApotekaID",
                principalTable: "Apoteka",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
