using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class v48 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek");

            migrationBuilder.DropForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Klijent",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Klijent",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Klijent",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek",
                column: "ReceptID",
                principalTable: "Recept",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept",
                column: "KlijentID",
                principalTable: "Klijent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek");

            migrationBuilder.DropForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Klijent",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Klijent",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Klijent",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lek_Recept_ReceptID",
                table: "Lek",
                column: "ReceptID",
                principalTable: "Recept",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recept_Klijent_KlijentID",
                table: "Recept",
                column: "KlijentID",
                principalTable: "Klijent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
