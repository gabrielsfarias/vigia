using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vigia.Migrations
{
    /// <inheritdoc />
    public partial class UsaUsuarioComoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_AspNetUsers_UsuarioId",
                table: "Documentos"
            );

            migrationBuilder.DropColumn(name: "Discriminator", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "Nome", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "Senha", table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new { Id = table.Column<string>(type: "TEXT", nullable: false) },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Usuarios_UsuarioId",
                table: "Documentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Usuarios_UsuarioId",
                table: "Documentos"
            );

            migrationBuilder.DropTable(name: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_AspNetUsers_UsuarioId",
                table: "Documentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
