using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LTMSistem.Migrations
{
    public partial class RelacionamentoEntreLembretesEUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lembretes_Usuarios_UsuarioId",
                table: "Lembretes");

            migrationBuilder.DropIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Lembretes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lembretes_Usuarios_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lembretes_Usuarios_UsuarioId",
                table: "Lembretes");

            migrationBuilder.DropIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Lembretes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Lembretes_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lembretes_Usuarios_UsuarioId",
                table: "Lembretes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
