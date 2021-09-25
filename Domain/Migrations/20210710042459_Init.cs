using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Casa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadCuartos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escuela",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escuela", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscuelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Escuela_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuela",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grado = table.Column<int>(type: "int", nullable: false),
                    EscuelaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiante_Escuela_EscuelaId",
                        column: x => x.EscuelaId,
                        principalTable: "Escuela",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    EstudianteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaFinal = table.Column<double>(type: "float", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => new { x.EstudianteId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Matricula_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matricula_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_EscuelaId",
                table: "Curso",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_EscuelaId",
                table: "Estudiante",
                column: "EscuelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_CursoId",
                table: "Matricula",
                column: "CursoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Casa");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "Escuela");
        }
    }
}
