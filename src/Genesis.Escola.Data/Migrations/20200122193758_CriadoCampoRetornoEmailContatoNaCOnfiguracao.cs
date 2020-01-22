using Microsoft.EntityFrameworkCore.Migrations;

namespace Genesis.Escola.Data.Migrations
{
    public partial class CriadoCampoRetornoEmailContatoNaCOnfiguracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailRetContato",
                table: "Config",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailRetContato",
                table: "Config");
        }
    }
}
