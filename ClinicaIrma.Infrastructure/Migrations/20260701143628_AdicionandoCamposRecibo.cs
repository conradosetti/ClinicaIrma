using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaIrma.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCamposRecibo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReciboReceitaFederalEmitido",
                table: "Pagamentos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UrlReciboGerado",
                table: "Pagamentos",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReciboReceitaFederalEmitido",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "UrlReciboGerado",
                table: "Pagamentos");
        }
    }
}
