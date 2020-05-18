using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kalum2020_v1.Migrations
{
    public partial class createTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarreraTecnica",
                columns: table => new
                {
                    CarreraTecnicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCarrera = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarreraTecnica", x => x.CarreraTecnicaId);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    HorarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorarioInicio = table.Column<DateTime>(nullable: false),
                    HorarioFinal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.HorarioId);
                });

            migrationBuilder.CreateTable(
                name: "Instructores",
                columns: table => new
                {
                    InstructorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Estatus = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructores", x => x.InstructorId);
                });

            migrationBuilder.CreateTable(
                name: "Religiones",
                columns: table => new
                {
                    ReligionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religiones", x => x.ReligionId);
                });

            migrationBuilder.CreateTable(
                name: "Salones",
                columns: table => new
                {
                    SalonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreSalon = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Capacidad = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salones", x => x.SalonId);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    AlumnoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Carne = table.Column<int>(nullable: false),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    fecha_nacimiento = table.Column<DateTime>(nullable: false),
                    ReligionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.AlumnoId);
                    table.ForeignKey(
                        name: "FK_Alumnos_Religiones_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "Religiones",
                        principalColumn: "ReligionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    ClaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true),
                    Ciclo = table.Column<int>(nullable: false),
                    SalonId = table.Column<int>(nullable: true),
                    HorarioId = table.Column<int>(nullable: true),
                    InstructorId = table.Column<int>(nullable: true),
                    CarreraTecnicaId = table.Column<int>(nullable: true),
                    CupoMinimo = table.Column<int>(nullable: false),
                    CupoMaximo = table.Column<int>(nullable: false),
                    CantidadAsignaciones = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.ClaseId);
                    table.ForeignKey(
                        name: "FK_Clase_CarreraTecnica_CarreraTecnicaId",
                        column: x => x.CarreraTecnicaId,
                        principalTable: "CarreraTecnica",
                        principalColumn: "CarreraTecnicaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clase_Horarios_HorarioId",
                        column: x => x.HorarioId,
                        principalTable: "Horarios",
                        principalColumn: "HorarioId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clase_Instructores_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructores",
                        principalColumn: "InstructorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clase_Salones_SalonId",
                        column: x => x.SalonId,
                        principalTable: "Salones",
                        principalColumn: "SalonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AsignacionAlumno",
                columns: table => new
                {
                    AsignacionAlumnoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClasesClaseId = table.Column<int>(nullable: true),
                    AlumnoId = table.Column<int>(nullable: true),
                    FechaAsignacion = table.Column<DateTime>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionAlumno", x => x.AsignacionAlumnoId);
                    table.ForeignKey(
                        name: "FK_AsignacionAlumno_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "AlumnoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AsignacionAlumno_Clase_ClasesClaseId",
                        column: x => x.ClasesClaseId,
                        principalTable: "Clase",
                        principalColumn: "ClaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_ReligionId",
                table: "Alumnos",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionAlumno_AlumnoId",
                table: "AsignacionAlumno",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionAlumno_ClasesClaseId",
                table: "AsignacionAlumno",
                column: "ClasesClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_CarreraTecnicaId",
                table: "Clase",
                column: "CarreraTecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_HorarioId",
                table: "Clase",
                column: "HorarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_InstructorId",
                table: "Clase",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_SalonId",
                table: "Clase",
                column: "SalonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionAlumno");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "Religiones");

            migrationBuilder.DropTable(
                name: "CarreraTecnica");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Instructores");

            migrationBuilder.DropTable(
                name: "Salones");
        }
    }
}
