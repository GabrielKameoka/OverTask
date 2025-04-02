using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OverTask.api.Migrations
{
    /// <inheritdoc />
    public partial class ModelsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuariosId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuariosId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "Tarefas");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Usuarios_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas");

            migrationBuilder.AddColumn<int>(
                name: "UsuariosId",
                table: "Tarefas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuariosId",
                table: "Tarefas",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Usuarios_UsuariosId",
                table: "Tarefas",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
