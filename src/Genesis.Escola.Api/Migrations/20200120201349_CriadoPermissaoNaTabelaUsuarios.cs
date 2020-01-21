using Microsoft.EntityFrameworkCore.Migrations;

namespace Genesis.Escola.Api.Migrations
{
    public partial class CriadoPermissaoNaTabelaUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Permissao",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissao",
                table: "AspNetUsers");
        }
    }
}
