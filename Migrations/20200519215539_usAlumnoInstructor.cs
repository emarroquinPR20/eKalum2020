using Microsoft.EntityFrameworkCore.Migrations;

namespace kalum2020_v1.Migrations
{
    public partial class usAlumnoInstructor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comentario",
                table: "Instructores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroExpediente",
                table: "Alumnos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comentario",
                table: "Instructores");

            migrationBuilder.DropColumn(
                name: "NumeroExpediente",
                table: "Alumnos");
        }
    }
}
