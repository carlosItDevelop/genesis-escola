using Microsoft.EntityFrameworkCore.Migrations;

namespace Genesis.Escola.Data.Migrations
{
    public partial class CriadoCampoLinkYoutubeeImagemYoutubeNaCOnfiguracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemYoutube",
                table: "Config",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkYoutube",
                table: "Config",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemYoutube",
                table: "Config");

            migrationBuilder.DropColumn(
                name: "LinkYoutube",
                table: "Config");
        }
    }
}
