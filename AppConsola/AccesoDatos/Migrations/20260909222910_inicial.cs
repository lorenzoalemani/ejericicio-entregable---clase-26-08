using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Socio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", nullable: false),
                    Dni = table.Column<int>(type: "INTEGER", nullable: false),
                    Telefono = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alquiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Socioid = table.Column<int>(type: "INTEGER", nullable: false),
                    Fechaentrega = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fechadevolucion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquiler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alquiler_Socio_Socioid",
                        column: x => x.Socioid,
                        principalTable: "Socio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pelicula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Autor = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    AlquilerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelicula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pelicula_Alquiler_AlquilerId",
                        column: x => x.AlquilerId,
                        principalTable: "Alquiler",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquiler_Socioid",
                table: "Alquiler",
                column: "Socioid");

            migrationBuilder.CreateIndex(
                name: "IX_Pelicula_AlquilerId",
                table: "Pelicula",
                column: "AlquilerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pelicula");

            migrationBuilder.DropTable(
                name: "Alquiler");

            migrationBuilder.DropTable(
                name: "Socio");
        }
    }
}
