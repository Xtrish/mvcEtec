using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTMSistem.Migrations
{
    public partial class RelacionamentoEntreLembretesEUsuario1ForN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes");

            migrationBuilder.CreateIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes");

            migrationBuilder.CreateIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId",
                unique: true);
        }
    }
}
