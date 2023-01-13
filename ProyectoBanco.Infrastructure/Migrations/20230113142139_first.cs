using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoBanco.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    CliId = table.Column<int>(name: "Cli_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CliIdentificacion = table.Column<string>(name: "Cli_Identificacion", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliApellido1 = table.Column<string>(name: "Cli_Apellido1", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliApellido2 = table.Column<string>(name: "Cli_Apellido2", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliNombre1 = table.Column<string>(name: "Cli_Nombre1", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliNombre2 = table.Column<string>(name: "Cli_Nombre2", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliDireccion = table.Column<string>(name: "Cli_Direccion", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CliCiudad = table.Column<string>(name: "Cli_Ciudad", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliCelular = table.Column<string>(name: "Cli_Celular", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliEmail = table.Column<string>(name: "Cli_Email", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.CliId);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    CueId = table.Column<int>(name: "Cue_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CueNumero = table.Column<string>(name: "Cue_Numero", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CliId = table.Column<int>(name: "Cli_Id", type: "int", nullable: false),
                    CueActiva = table.Column<bool>(name: "Cue_Activa", type: "bit", nullable: false),
                    CueFechaCreacion = table.Column<DateTime>(name: "Cue_FechaCreacion", type: "datetime", nullable: false),
                    CueUsuarioCreacion = table.Column<string>(name: "Cue_UsuarioCreacion", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CueSaldoActual = table.Column<decimal>(name: "Cue_SaldoActual", type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.CueId);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes",
                        column: x => x.CliId,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id");
                });

            migrationBuilder.CreateTable(
                name: "Seguridad",
                columns: table => new
                {
                    SegId = table.Column<int>(name: "Seg_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegUsu = table.Column<string>(name: "Seg_Usu", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    SegNombreUsuario = table.Column<string>(name: "Seg_NombreUsuario", type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SegContrasena = table.Column<string>(name: "Seg_Contrasena", type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    SegRol = table.Column<string>(name: "Seg_Rol", type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SegCliId = table.Column<int>(name: "Seg_CliId", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguridad", x => x.SegId);
                    table.ForeignKey(
                        name: "FK_Seguridad_Clientes_Seg_CliId",
                        column: x => x.SegCliId,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    MovId = table.Column<int>(name: "Mov_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovFecha = table.Column<DateTime>(name: "Mov_Fecha", type: "datetime", nullable: false),
                    MovOrigen = table.Column<string>(name: "Mov_Origen", type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MovValor = table.Column<decimal>(name: "Mov_Valor", type: "decimal(18,0)", nullable: false),
                    MovTipo = table.Column<string>(name: "Mov_Tipo", type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CliId = table.Column<int>(name: "Cli_Id", type: "int", nullable: false),
                    CueId = table.Column<int>(name: "Cue_Id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.MovId);
                    table.ForeignKey(
                        name: "FK_Movimientos_Clientes",
                        column: x => x.CliId,
                        principalTable: "Clientes",
                        principalColumn: "Cli_Id");
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuentas",
                        column: x => x.CueId,
                        principalTable: "Cuentas",
                        principalColumn: "Cue_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_Cli_Id",
                table: "Cuentas",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Cli_Id",
                table: "Movimientos",
                column: "Cli_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_Cue_Id",
                table: "Movimientos",
                column: "Cue_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seguridad_Seg_CliId",
                table: "Seguridad",
                column: "Seg_CliId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Seguridad");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
