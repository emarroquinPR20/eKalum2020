using Microsoft.EntityFrameworkCore.Migrations;

namespace kalum2020_v1.Migrations
{
    public partial class ModifyNamesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Religiones_ReligionId",
                table: "Alumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionAlumno_Alumnos_AlumnoId",
                table: "AsignacionAlumno");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionAlumno_Clase_ClasesClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.DropForeignKey(
                name: "FK_Clase_CarreraTecnica_CarreraTecnicaId",
                table: "Clase");

            migrationBuilder.DropForeignKey(
                name: "FK_Clase_Horarios_HorarioId",
                table: "Clase");

            migrationBuilder.DropForeignKey(
                name: "FK_Clase_Instructores_InstructorId",
                table: "Clase");

            migrationBuilder.DropForeignKey(
                name: "FK_Clase_Salones_SalonId",
                table: "Clase");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionAlumno_ClasesClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clase",
                table: "Clase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarreraTecnica",
                table: "CarreraTecnica");

            migrationBuilder.DropColumn(
                name: "ClasesClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.RenameTable(
                name: "Clase",
                newName: "Clases");

            migrationBuilder.RenameTable(
                name: "CarreraTecnica",
                newName: "CarreraTecnicas");

            migrationBuilder.RenameIndex(
                name: "IX_Clase_SalonId",
                table: "Clases",
                newName: "IX_Clases_SalonId");

            migrationBuilder.RenameIndex(
                name: "IX_Clase_InstructorId",
                table: "Clases",
                newName: "IX_Clases_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Clase_HorarioId",
                table: "Clases",
                newName: "IX_Clases_HorarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Clase_CarreraTecnicaId",
                table: "Clases",
                newName: "IX_Clases_CarreraTecnicaId");

            migrationBuilder.AlterColumn<int>(
                name: "AlumnoId",
                table: "AsignacionAlumno",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClaseId",
                table: "AsignacionAlumno",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Alumnos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Clases",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Clases",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HorarioId",
                table: "Clases",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clases",
                table: "Clases",
                column: "ClaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarreraTecnicas",
                table: "CarreraTecnicas",
                column: "CarreraTecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionAlumno_ClaseId",
                table: "AsignacionAlumno",
                column: "ClaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Religiones_ReligionId",
                table: "Alumnos",
                column: "ReligionId",
                principalTable: "Religiones",
                principalColumn: "ReligionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionAlumno_Alumnos_AlumnoId",
                table: "AsignacionAlumno",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "AlumnoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionAlumno_Clases_ClaseId",
                table: "AsignacionAlumno",
                column: "ClaseId",
                principalTable: "Clases",
                principalColumn: "ClaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_CarreraTecnicas_CarreraTecnicaId",
                table: "Clases",
                column: "CarreraTecnicaId",
                principalTable: "CarreraTecnicas",
                principalColumn: "CarreraTecnicaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Horarios_HorarioId",
                table: "Clases",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "HorarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Instructores_InstructorId",
                table: "Clases",
                column: "InstructorId",
                principalTable: "Instructores",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clases_Salones_SalonId",
                table: "Clases",
                column: "SalonId",
                principalTable: "Salones",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alumnos_Religiones_ReligionId",
                table: "Alumnos");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionAlumno_Alumnos_AlumnoId",
                table: "AsignacionAlumno");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionAlumno_Clases_ClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_CarreraTecnicas_CarreraTecnicaId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Horarios_HorarioId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Instructores_InstructorId",
                table: "Clases");

            migrationBuilder.DropForeignKey(
                name: "FK_Clases_Salones_SalonId",
                table: "Clases");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionAlumno_ClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clases",
                table: "Clases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarreraTecnicas",
                table: "CarreraTecnicas");

            migrationBuilder.DropColumn(
                name: "ClaseId",
                table: "AsignacionAlumno");

            migrationBuilder.RenameTable(
                name: "Clases",
                newName: "Clase");

            migrationBuilder.RenameTable(
                name: "CarreraTecnicas",
                newName: "CarreraTecnica");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_SalonId",
                table: "Clase",
                newName: "IX_Clase_SalonId");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_InstructorId",
                table: "Clase",
                newName: "IX_Clase_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_HorarioId",
                table: "Clase",
                newName: "IX_Clase_HorarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Clases_CarreraTecnicaId",
                table: "Clase",
                newName: "IX_Clase_CarreraTecnicaId");

            migrationBuilder.AlterColumn<int>(
                name: "AlumnoId",
                table: "AsignacionAlumno",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ClasesClaseId",
                table: "AsignacionAlumno",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "Alumnos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Clase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Clase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HorarioId",
                table: "Clase",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clase",
                table: "Clase",
                column: "ClaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarreraTecnica",
                table: "CarreraTecnica",
                column: "CarreraTecnicaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionAlumno_ClasesClaseId",
                table: "AsignacionAlumno",
                column: "ClasesClaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alumnos_Religiones_ReligionId",
                table: "Alumnos",
                column: "ReligionId",
                principalTable: "Religiones",
                principalColumn: "ReligionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionAlumno_Alumnos_AlumnoId",
                table: "AsignacionAlumno",
                column: "AlumnoId",
                principalTable: "Alumnos",
                principalColumn: "AlumnoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionAlumno_Clase_ClasesClaseId",
                table: "AsignacionAlumno",
                column: "ClasesClaseId",
                principalTable: "Clase",
                principalColumn: "ClaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clase_CarreraTecnica_CarreraTecnicaId",
                table: "Clase",
                column: "CarreraTecnicaId",
                principalTable: "CarreraTecnica",
                principalColumn: "CarreraTecnicaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clase_Horarios_HorarioId",
                table: "Clase",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "HorarioId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clase_Instructores_InstructorId",
                table: "Clase",
                column: "InstructorId",
                principalTable: "Instructores",
                principalColumn: "InstructorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clase_Salones_SalonId",
                table: "Clase",
                column: "SalonId",
                principalTable: "Salones",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
