using Microsoft.EntityFrameworkCore.Migrations;

namespace Genesis.Escola.Data.Migrations
{
    public partial class AlteradoTabelasTarefaseCronogramas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TurmaId",
                table: "Tarefas",
                type: "varchar(500)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "TurmaId",
                table: "Cronogramas",
                type: "varchar(500)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(9)",
                oldMaxLength: 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TurmaId",
                table: "Tarefas",
                type: "varchar(9)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 7);

            migrationBuilder.AlterColumn<string>(
                name: "TurmaId",
                table: "Cronogramas",
                type: "varchar(9)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 7);
        }
    }
}
