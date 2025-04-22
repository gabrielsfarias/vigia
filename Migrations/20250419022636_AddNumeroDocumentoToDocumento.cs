using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vigia.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroDocumentoToDocumento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroDocumento",
                table: "Documentos",
                type: "TEXT",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "NumeroDocumento", table: "Documentos");
        }
    }
}
