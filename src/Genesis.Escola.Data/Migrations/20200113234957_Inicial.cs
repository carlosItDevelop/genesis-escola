using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Genesis.Escola.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Senha = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Curso = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Serie = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Turma = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    Turno = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Numero = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DtNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    TextoFinalBoletim = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    RespNome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RespEndereco = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    RespCidade = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    RespEstado = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    RespCep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true),
                    RespEmail = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Simbolo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Ativo = table.Column<bool>(nullable: false, defaultValueSql: "0"),
                    TurmaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carrossel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Resumo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Ativo = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrossel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comunicados",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoResumida = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "date", nullable: true),
                    DescricaoCompleta = table.Column<string>(type: "text", nullable: true),
                    TurmaId = table.Column<string>(type: "varchar(500)", maxLength: 7, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TituloContato = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DescricaoContato = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    TituloTrabalhe = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    DescricaoTrabalhe = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    Telefones = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmailRetTrabalhe = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmailEnvio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmailSenha = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmailHost = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EmailPorta = table.Column<int>(type: "integer", nullable: false),
                    EmailSsl = table.Column<bool>(nullable: false, defaultValue: true),
                    MensagemRetContato = table.Column<string>(type: "text", nullable: false),
                    MensagemRetTrabalhe = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoResumida = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "date", nullable: true),
                    DescricaoCompleta = table.Column<string>(type: "text", nullable: true),
                    TurmaId = table.Column<string>(type: "varchar(9)", maxLength: 7, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CursoAcadesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ciclo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Codigo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NumSeries = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoAcadesc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Curso = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Sigla = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    CargaHoraria = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Professor = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Cargo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Curso = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Serie = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    Turma = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailContato",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailContato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filosofia",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filosofia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Galeria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galeria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Matricula = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    Disciplina = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Nota1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nota2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nota3 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nota4 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nt1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nt2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nt3 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Nt4 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRec1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRec2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRec3 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRec4 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRecSem1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRecSem2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MediaSem1 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MediaSem2 = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Cor1 = table.Column<bool>(nullable: false),
                    Cor2 = table.Column<bool>(nullable: false),
                    Cor3 = table.Column<bool>(nullable: false),
                    Cor4 = table.Column<bool>(nullable: false),
                    CorRec = table.Column<bool>(nullable: false),
                    MediaBim = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NotaRec = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MediaFinal = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Situacao = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CorSituacao = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Falta1 = table.Column<short>(type: "SMALLINT", nullable: false, defaultValueSql: "0"),
                    Falta2 = table.Column<short>(type: "SMALLINT", nullable: false, defaultValueSql: "0"),
                    Falta3 = table.Column<short>(type: "SMALLINT", nullable: false, defaultValueSql: "0"),
                    Falta4 = table.Column<short>(type: "SMALLINT", nullable: false, defaultValueSql: "0"),
                    PerFalta = table.Column<double>(nullable: false, defaultValueSql: "0"),
                    PerFrequencia = table.Column<double>(nullable: false, defaultValueSql: "0"),
                    TotalFaltas = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    Observacoes = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Noticias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoResumida = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "date", nullable: true),
                    DescricaoCompleta = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    CaminhoImagem1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CaminhoImagem2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CaminhoImagem3 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    NomeCurso1 = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    NomeCurso2 = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    NomeCurso3 = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    DescricaoCurso1 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    DescricaoCurso2 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    DescricaoCurso3 = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    LinkCurso1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LinkCurso2 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    LinkCurso3 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sobre",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoResumida = table.Column<string>(type: "varchar(800)", maxLength: 800, nullable: false),
                    SobreCompleto = table.Column<string>(type: "text", nullable: false),
                    CaminhoImagemPrincipal = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sobre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoResumida = table.Column<string>(type: "varchar(600)", maxLength: 600, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: true),
                    DataFinal = table.Column<DateTime>(type: "date", nullable: true),
                    DescricaoCompleta = table.Column<string>(type: "text", nullable: true),
                    TurmaId = table.Column<string>(type: "varchar(9)", maxLength: 7, nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Sigla = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurmaAcadesc",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ciclo = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Serie = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    Turma = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Turno = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAcadesc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GaleriaItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Resumo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    GaleriaId = table.Column<Guid>(nullable: false),
                    CaminhoImagem = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GaleriaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GaleriaItem_Galeria_GaleriaId",
                        column: x => x.GaleriaId,
                        principalTable: "Galeria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GaleriaItem_GaleriaId",
                table: "GaleriaItem",
                column: "GaleriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Carrossel");

            migrationBuilder.DropTable(
                name: "Comunicados");

            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "CursoAcadesc");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "EmailContato");

            migrationBuilder.DropTable(
                name: "Filosofia");

            migrationBuilder.DropTable(
                name: "GaleriaItem");

            migrationBuilder.DropTable(
                name: "Missao");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Noticias");

            migrationBuilder.DropTable(
                name: "Polo");

            migrationBuilder.DropTable(
                name: "Sobre");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "TurmaAcadesc");

            migrationBuilder.DropTable(
                name: "Galeria");
        }
    }
}
