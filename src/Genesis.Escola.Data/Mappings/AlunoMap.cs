using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id");
              //  .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Matricula)
                   .HasMaxLength(8)
                   .HasColumnType("varchar(8)")
                   .HasColumnName("Matricula");

            builder.Property(a => a.Nome)
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)")
                   .HasColumnName("Nome");

            builder.Property(a => a.Senha)
                   .HasMaxLength(15)
                   .HasColumnType("varchar(15)")
                   .HasColumnName("Senha");

            builder.Property(a => a.Curso)
                   .HasMaxLength(3)
                   .HasColumnType("varchar(3)")
                   .HasColumnName("Curso");

            builder.Property(a => a.Serie)
                   .HasMaxLength(1)
                   .HasColumnType("varchar(1)")
                   .HasColumnName("Serie");

            builder.Property(a => a.Turma)
                   .HasMaxLength(5)
                   .HasColumnType("varchar(5)")
                   .HasColumnName("Turma");

            builder.Property(a => a.Turno)
                   .HasMaxLength(1)
                   .HasColumnType("varchar(1)")
                   .HasColumnName("Turno");

            builder.Property(a => a.Numero)
                   .HasMaxLength(3)
                   .HasColumnType("varchar(3)")
                   .HasColumnName("Numero");

            builder.Property(a => a.DtNascimento)
                   .HasColumnType("date")
                   .HasColumnName("DtNascimento");

            builder.Property(a => a.TextoFinalBoletim)
                   .HasMaxLength(250)
                   .HasColumnType("varchar(250)")
                   .HasColumnName("TextoFinalBoletim");

            builder.Property(a => a.RespNome)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .HasColumnName("RespNome");

            builder.Property(a => a.RespEndereco)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .HasColumnName("RespEndereco");

            builder.Property(a => a.RespCidade)
                   .HasMaxLength(30)
                   .HasColumnType("varchar(30)")
                   .HasColumnName("RespCidade");

            builder.Property(a => a.RespEstado)
                   .HasMaxLength(2)
                   .HasColumnType("varchar(2)")
                   .HasColumnName("RespEstado");

            builder.Property(a => a.RespCep)
                   .HasMaxLength(9)
                   .HasColumnType("varchar(9)")
                   .HasColumnName("RespCep");

            builder.Property(a => a.RespEmail)
                   .HasMaxLength(50)
                   .HasColumnType("varchar(50)")
                   .HasColumnName("RespEmail");

            builder.Property(a => a.Simbolo)
                   .HasMaxLength(1)
                   .HasColumnType("varchar(1)")
                   .HasColumnName("Simbolo");

            builder.Property(e => e.Ativo)
                   .HasColumnName("Ativo")
                   .HasDefaultValueSql("0");

            builder.Property(e => e.TurmaId)
                   .HasColumnName("TurmaId");
                 //  .HasColumnType("UniqueIdentifier");

            //builder.HasOne(a => a.TurmaSite)
            //       .WithMany(a => a.Alunos)
            //       .HasForeignKey(a => a.TurmaId)
            //       .HasPrincipalKey(a => a.Id);


            builder.ToTable("Alunos");
        }
    }
}
