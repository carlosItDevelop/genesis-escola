﻿// <auto-generated />
using System;
using Genesis.Escola.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Genesis.Escola.Data.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200122193758_CriadoCampoRetornoEmailContatoNaCOnfiguracao")]
    partial class CriadoCampoRetornoEmailContatoNaCOnfiguracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Genesis.Escola.Business.Models.Aluno", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Ativo")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Curso")
                        .HasColumnName("Curso")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<DateTime>("DtNascimento")
                        .HasColumnName("DtNascimento")
                        .HasColumnType("date");

                    b.Property<string>("Matricula")
                        .HasColumnName("Matricula")
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Numero")
                        .HasColumnName("Numero")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("RespCep")
                        .HasColumnName("RespCep")
                        .HasColumnType("varchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("RespCidade")
                        .HasColumnName("RespCidade")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("RespEmail")
                        .HasColumnName("RespEmail")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RespEndereco")
                        .HasColumnName("RespEndereco")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RespEstado")
                        .HasColumnName("RespEstado")
                        .HasColumnType("varchar(2)")
                        .HasMaxLength(2);

                    b.Property<string>("RespNome")
                        .HasColumnName("RespNome")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Senha")
                        .HasColumnName("Senha")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Serie")
                        .HasColumnName("Serie")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Simbolo")
                        .HasColumnName("Simbolo")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("TextoFinalBoletim")
                        .HasColumnName("TextoFinalBoletim")
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("Turma")
                        .HasColumnName("Turma")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<Guid>("TurmaId")
                        .HasColumnName("TurmaId");

                    b.Property<string>("Turno")
                        .HasColumnName("Turno")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Carrossel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Ativo")
                        .HasColumnName("Ativo")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("CaminhoImagem")
                        .IsRequired()
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Resumo")
                        .IsRequired()
                        .HasColumnName("Resumo")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Carrossel");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Comunicado", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem")
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnName("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnName("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("DescricaoCompleta")
                        .HasColumnName("DescricaoCompleta")
                        .HasColumnType("longtext");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasColumnName("DescricaoResumida")
                        .HasColumnType("varchar(600)")
                        .HasMaxLength(600);

                    b.Property<string>("TurmaId")
                        .IsRequired()
                        .HasColumnName("TurmaId")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.ToTable("Comunicados");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Config", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("DescricaoContato")
                        .IsRequired()
                        .HasColumnName("DescricaoContato")
                        .HasColumnType("varchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("DescricaoTrabalhe")
                        .IsRequired()
                        .HasColumnName("DescricaoTrabalhe")
                        .HasColumnType("varchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("EmailEnvio")
                        .IsRequired()
                        .HasColumnName("EmailEnvio")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("EmailHost")
                        .IsRequired()
                        .HasColumnName("EmailHost")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("EmailPorta")
                        .HasColumnName("EmailPorta")
                        .HasColumnType("integer");

                    b.Property<string>("EmailRetContato")
                        .IsRequired()
                        .HasColumnName("EmailRetContato")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("EmailRetTrabalhe")
                        .IsRequired()
                        .HasColumnName("EmailRetTrabalhe")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("EmailSenha")
                        .IsRequired()
                        .HasColumnName("EmailSenha")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("EmailSsl")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmailSsl")
                        .HasDefaultValue(true);

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnName("Endereco")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("MensagemRetContato")
                        .IsRequired()
                        .HasColumnName("MensagemRetContato")
                        .HasColumnType("longtext");

                    b.Property<string>("MensagemRetTrabalhe")
                        .IsRequired()
                        .HasColumnName("MensagemRetTrabalhe")
                        .HasColumnType("longtext");

                    b.Property<string>("Telefones")
                        .IsRequired()
                        .HasColumnName("Telefones")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TituloContato")
                        .IsRequired()
                        .HasColumnName("TituloContato")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TituloTrabalhe")
                        .IsRequired()
                        .HasColumnName("TituloTrabalhe")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Config");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Cronograma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem")
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnName("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnName("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("DescricaoCompleta")
                        .HasColumnName("DescricaoCompleta")
                        .HasColumnType("longtext");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasColumnName("DescricaoResumida")
                        .HasColumnType("varchar(600)")
                        .HasMaxLength(600);

                    b.Property<string>("TurmaId")
                        .IsRequired()
                        .HasColumnName("TurmaId")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.ToTable("Cronogramas");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.CursoAcadesc", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Ciclo")
                        .HasColumnName("Ciclo")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Codigo")
                        .HasColumnName("Codigo")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("NumSeries")
                        .HasColumnName("NumSeries")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CursoAcadesc");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Cursos", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("Curso")
                        .HasColumnName("Curso");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Disciplinas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CargaHoraria")
                        .HasColumnName("CargaHoraria")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Cargo")
                        .HasColumnName("Cargo")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Codigo")
                        .HasColumnName("Codigo")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Curso")
                        .HasColumnName("Curso")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Professor")
                        .HasColumnName("Professor")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Serie")
                        .HasColumnName("Serie")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Sigla")
                        .HasColumnName("Sigla")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Turma")
                        .HasColumnName("Turma")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("Disciplinas");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.EmailContato", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Nome")
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("EmailContato");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Filosofia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Filosofia");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Galeria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem")
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("varchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Galeria");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.GaleriaItens", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem")
                        .IsRequired()
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<Guid>("GaleriaId");

                    b.Property<string>("Resumo")
                        .HasColumnName("Resumo")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("GaleriaId");

                    b.ToTable("GaleriaItem");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Missao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasColumnType("longtext");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.ToTable("Missao");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Notas", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<bool>("Cor1")
                        .HasColumnName("Cor1");

                    b.Property<bool>("Cor2")
                        .HasColumnName("Cor2");

                    b.Property<bool>("Cor3")
                        .HasColumnName("Cor3");

                    b.Property<bool>("Cor4")
                        .HasColumnName("Cor4");

                    b.Property<bool>("CorRec")
                        .HasColumnName("CorRec");

                    b.Property<string>("CorSituacao")
                        .HasColumnName("CorSituacao")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Disciplina")
                        .HasColumnName("Disciplina")
                        .HasColumnType("varchar(3)")
                        .HasMaxLength(3);

                    b.Property<short>("Falta1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Falta1")
                        .HasColumnType("SMALLINT")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Falta2")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Falta2")
                        .HasColumnType("SMALLINT")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Falta3")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Falta3")
                        .HasColumnType("SMALLINT")
                        .HasDefaultValueSql("0");

                    b.Property<short>("Falta4")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Falta4")
                        .HasColumnType("SMALLINT")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Matricula")
                        .HasColumnName("Matricula")
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8);

                    b.Property<string>("MediaBim")
                        .HasColumnName("MediaBim")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MediaFinal")
                        .HasColumnName("MediaFinal")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MediaSem1")
                        .HasColumnName("MediaSem1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MediaSem2")
                        .HasColumnName("MediaSem2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nota1")
                        .HasColumnName("Nota1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nota2")
                        .HasColumnName("Nota2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nota3")
                        .HasColumnName("Nota3")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nota4")
                        .HasColumnName("Nota4")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRec")
                        .HasColumnName("NotaRec")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRec1")
                        .HasColumnName("NotaRec1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRec2")
                        .HasColumnName("NotaRec2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRec3")
                        .HasColumnName("NotaRec3")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRec4")
                        .HasColumnName("NotaRec4")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRecSem1")
                        .HasColumnName("NotaRecSem1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("NotaRecSem2")
                        .HasColumnName("NotaRecSem2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nt1")
                        .HasColumnName("Nt1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nt2")
                        .HasColumnName("Nt2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nt3")
                        .HasColumnName("Nt3")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nt4")
                        .HasColumnName("Nt4")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Observacoes")
                        .HasColumnName("Observacoes")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<double>("PerFalta")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PerFalta")
                        .HasDefaultValueSql("0");

                    b.Property<double>("PerFrequencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PerFrequencia")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Situacao")
                        .HasColumnName("Situacao")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("TotalFaltas")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TotalFaltas")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Noticia", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnName("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnName("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("DescricaoCompleta")
                        .IsRequired()
                        .HasColumnName("DescricaoCompleta")
                        .HasColumnType("longtext");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasColumnName("DescricaoResumida")
                        .HasColumnType("varchar(600)")
                        .HasMaxLength(600);

                    b.HasKey("Id");

                    b.ToTable("Noticias");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Polo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem1")
                        .IsRequired()
                        .HasColumnName("CaminhoImagem1")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("CaminhoImagem2")
                        .IsRequired()
                        .HasColumnName("CaminhoImagem2")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("CaminhoImagem3")
                        .IsRequired()
                        .HasColumnName("CaminhoImagem3")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("DescricaoCurso1")
                        .IsRequired()
                        .HasColumnName("DescricaoCurso1")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("DescricaoCurso2")
                        .IsRequired()
                        .HasColumnName("DescricaoCurso2")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("DescricaoCurso3")
                        .IsRequired()
                        .HasColumnName("DescricaoCurso3")
                        .HasColumnType("varchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("LinkCurso1")
                        .IsRequired()
                        .HasColumnName("LinkCurso1")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LinkCurso2")
                        .IsRequired()
                        .HasColumnName("LinkCurso2")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("LinkCurso3")
                        .IsRequired()
                        .HasColumnName("LinkCurso3")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("NomeCurso1")
                        .IsRequired()
                        .HasColumnName("NomeCurso1")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("NomeCurso2")
                        .IsRequired()
                        .HasColumnName("NomeCurso2")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("NomeCurso3")
                        .IsRequired()
                        .HasColumnName("NomeCurso3")
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("Titulo")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Polo");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Sobre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagemPrincipal")
                        .IsRequired()
                        .HasColumnName("CaminhoImagemPrincipal")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("SobreCompleto")
                        .IsRequired()
                        .HasColumnName("SobreCompleto")
                        .HasColumnType("longtext");

                    b.Property<string>("SobreResumido")
                        .IsRequired()
                        .HasColumnName("DescricaoResumida")
                        .HasColumnType("varchar(800)")
                        .HasMaxLength(800);

                    b.HasKey("Id");

                    b.ToTable("Sobre");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("CaminhoImagem")
                        .HasColumnName("CaminhoImagem")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnName("DataFinal")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DataInicio")
                        .HasColumnName("DataInicio")
                        .HasColumnType("date");

                    b.Property<string>("DescricaoCompleta")
                        .HasColumnName("DescricaoCompleta")
                        .HasColumnType("longtext");

                    b.Property<string>("DescricaoResumida")
                        .IsRequired()
                        .HasColumnName("DescricaoResumida")
                        .HasColumnType("varchar(600)")
                        .HasMaxLength(600);

                    b.Property<string>("TurmaId")
                        .IsRequired()
                        .HasColumnName("TurmaId")
                        .HasColumnType("varchar(500)")
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.Turma", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("Sigla")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.TurmaAcadesc", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Ciclo")
                        .IsRequired()
                        .HasColumnName("Ciclo")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Serie")
                        .IsRequired()
                        .HasColumnName("Serie")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Turma")
                        .IsRequired()
                        .HasColumnName("Turma")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Turno")
                        .IsRequired()
                        .HasColumnName("Turno")
                        .HasColumnType("varchar(1)")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("TurmaAcadesc");
                });

            modelBuilder.Entity("Genesis.Escola.Business.Models.GaleriaItens", b =>
                {
                    b.HasOne("Genesis.Escola.Business.Models.Galeria", "Galeria")
                        .WithMany("galeriaItens")
                        .HasForeignKey("GaleriaId");
                });
#pragma warning restore 612, 618
        }
    }
}