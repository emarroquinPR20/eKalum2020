using Microsoft.EntityFrameworkCore.Migrations;

namespace kalum2020_v1.Migrations
{
    public partial class ModifyNamesTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_CarreraTecnicas_CarreraTecnicaId",
                table: "Clases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarreraTecnicas",
                table: "CarreraTecnicas");

            migrationBuilder.RenameTable(
                name: "CarreraTecnicas",
                newName: "CarrerasTecnicas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarrerasTecnicas",
                table: "CarrerasTecnicas",
                column: "CarreraTecnicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_CarrerasTecnicas_CarreraTecnicaId",
                table: "Clases",
                column: "CarreraTecnicaId",
                principalTable: "CarrerasTecnicas",
                principalColumn: "CarreraTecnicaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clases_CarrerasTecnicas_CarreraTecnicaId",
                table: "Clases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarrerasTecnicas",
                table: "CarrerasTecnicas");

            migrationBuilder.RenameTable(
                name: "CarrerasTecnicas",
                newName: "CarreraTecnicas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarreraTecnicas",
                table: "CarreraTecnicas",
                column: "CarreraTecnicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_CarreraTecnicas_CarreraTecnicaId",
                table: "Clases",
                column: "CarreraTecnicaId",
                principalTable: "CarreraTecnicas",
                principalColumn: "CarreraTecnicaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
