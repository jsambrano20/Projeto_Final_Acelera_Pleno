using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AceleraPleno.API.Migrations
{
    public partial class AdicionandoValorPrato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "Pratos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Pratos");
        }
    }
}
