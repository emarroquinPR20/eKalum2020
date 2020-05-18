using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kalum2020_v1.Migrations
{
    public partial class UpdateAlumnos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecha_nacimiento",
                table: "Alumnos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Alumnos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "Alumnos");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_nacimiento",
                table: "Alumnos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
