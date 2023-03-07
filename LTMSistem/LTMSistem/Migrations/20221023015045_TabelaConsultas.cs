using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTMSistem.Migrations
{
    public partial class TabelaConsultas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ConsultaModelId",
                table: "Usuarios",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsultaModelId",
                table: "Pacientes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioAlteracao = table.Column<long>(type: "bigint", nullable: true),
                    PacienteId = table.Column<long>(type: "bigint", nullable: false),
                    DentistaId = table.Column<long>(type: "bigint", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Procedimento = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ConsultaModelId",
                table: "Usuarios",
                column: "ConsultaModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_ConsultaModelId",
                table: "Pacientes",
                column: "ConsultaModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Consultas_ConsultaModelId",
                table: "Pacientes",
                column: "ConsultaModelId",
                principalTable: "Consultas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Consultas_ConsultaModelId",
                table: "Usuarios",
                column: "ConsultaModelId",
                principalTable: "Consultas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Consultas_ConsultaModelId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Consultas_ConsultaModelId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ConsultaModelId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_ConsultaModelId",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "ConsultaModelId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ConsultaModelId",
                table: "Pacientes");
        }
    }
}
