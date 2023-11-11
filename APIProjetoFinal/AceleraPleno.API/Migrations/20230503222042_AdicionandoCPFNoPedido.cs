using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AceleraPleno.API.Migrations
{
    public partial class AdicionandoCPFNoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Pedidos");
        }
    }
}
