using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class NotasMap : IEntityTypeConfiguration<Notas>
    {
        public void Configure(EntityTypeBuilder<Notas> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id");
             //   .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Matricula)
                   .HasMaxLength(8)
                   .HasColumnType("varchar(8)")
                   .HasColumnName("Matricula");

            builder.Property(a => a.Disciplina)
                   .HasMaxLength(3)
                   .HasColumnType("varchar(3)")
                   .HasColumnName("Disciplina");

            builder.Property(a => a.Nota1)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nota1");

            builder.Property(a => a.Nota2)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nota2");

            builder.Property(a => a.Nota3)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nota3");

            builder.Property(a => a.Nota4)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nota4");

            builder.Property(a => a.Nt1)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nt1");

            builder.Property(a => a.Nt2)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nt2");

            builder.Property(a => a.Nt3)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nt3");

            builder.Property(a => a.Nt4)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("Nt4");

            builder.Property(a => a.NotaRec1)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRec1");

            builder.Property(a => a.NotaRec2)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRec2");

            builder.Property(a => a.NotaRec3)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRec3");

            builder.Property(a => a.NotaRec4)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRec4");

            builder.Property(a => a.NotaRecSem1)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRecSem1");

            builder.Property(a => a.NotaRecSem2)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRecSem2");

            builder.Property(a => a.MediaSem1)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("MediaSem1");

            builder.Property(a => a.MediaSem2)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("MediaSem2");

            builder.Property(e => e.Cor1)
                .HasColumnName("Cor1");

            builder.Property(e => e.Cor2)
                .HasColumnName("Cor2");

            builder.Property(e => e.Cor3)
                .HasColumnName("Cor3");

            builder.Property(e => e.Cor4)
                .HasColumnName("Cor4");

            builder.Property(e => e.CorRec)
                .HasColumnName("CorRec");

            builder.Property(a => a.MediaBim)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("MediaBim");

            builder.Property(a => a.NotaRec)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("NotaRec");

            builder.Property(a => a.MediaFinal)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("MediaFinal");

            builder.Property(a => a.Situacao)
                   .HasMaxLength(20)
                   .HasColumnType("varchar(20)")
                   .HasColumnName("Situacao");

            builder.Property(a => a.CorSituacao)
                   .HasMaxLength(15)
                   .HasColumnType("varchar(15)")
                   .HasColumnName("CorSituacao");

            builder.Property(e => e.Falta1)
                .HasColumnName("Falta1")
                .HasColumnType("SMALLINT")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Falta2)
                .HasColumnName("Falta2")
                .HasColumnType("SMALLINT")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Falta3)
                .HasColumnName("Falta3")
                .HasColumnType("SMALLINT")
                .HasDefaultValueSql("0");

            builder.Property(e => e.Falta4)
                .HasColumnName("Falta4")
                .HasColumnType("SMALLINT")
                .HasDefaultValueSql("0");

            builder.Property(e => e.PerFalta)
                .HasColumnName("PerFalta")
                .HasDefaultValueSql("0");

            builder.Property(e => e.PerFrequencia)
                .HasColumnName("PerFrequencia")
                .HasDefaultValueSql("0");

            builder.Property(e => e.TotalFaltas)
                .HasColumnName("TotalFaltas")
                .HasDefaultValueSql("0");

            builder.Property(a => a.Observacoes)
                   .HasMaxLength(255)
                   .HasColumnType("varchar(255)")
                   .HasColumnName("Observacoes");

            builder.ToTable("Notas");
        }
    }
}
