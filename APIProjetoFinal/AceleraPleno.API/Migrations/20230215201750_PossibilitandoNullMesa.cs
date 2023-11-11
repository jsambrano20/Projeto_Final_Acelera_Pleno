using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AceleraPleno.API.Migrations
{
    public partial class PossibilitandoNullMesa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Clientes_ClienteId",
                table: "Mesas");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Mesas",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Clientes_ClienteId",
                table: "Mesas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mesas_Clientes_ClienteId",
                table: "Mesas");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "Mesas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mesas_Clientes_ClienteId",
                table: "Mesas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
