using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apoteka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoteka", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recept",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApotekaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recept", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Recept_Apoteka_ApotekaID",
                        column: x => x.ApotekaID,
                        principalTable: "Apoteka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lek",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    ReceptID = table.Column<int>(type: "int", nullable: true),
                    ApotekaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lek", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lek_Apoteka_ApotekaID",
                        column: x => x.ApotekaID,
                        principalTable: "Apoteka",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lek_Recept_ReceptID",
                        column: x => x.ReceptID,
                        principalTable: "Recept",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lek_ApotekaID",
                table: "Lek",
                column: "ApotekaID");

            migrationBuilder.CreateIndex(
                name: "IX_Lek_ReceptID",
                table: "Lek",
                column: "ReceptID");

            migrationBuilder.CreateIndex(
                name: "IX_Recept_ApotekaID",
                table: "Recept",
                column: "ApotekaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lek");

            migrationBuilder.DropTable(
                name: "Recept");

            migrationBuilder.DropTable(
                name: "Apoteka");
        }
    }
}
