using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class DisciplinasMap : IEntityTypeConfiguration<Disciplinas>
    {
        public void Configure(EntityTypeBuilder<Disciplinas> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
              ;//  .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Codigo)
                .HasMaxLength(3)
                .HasColumnType("varchar(3)")
                .HasColumnName("Codigo");

            builder.Property(a => a.Nome)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasColumnName("Nome");

            builder.Property(a => a.Sigla)
                   .HasMaxLength(5)
                   .HasColumnType("varchar(5)")
                   .HasColumnName("Sigla");

            builder.Property(a => a.CargaHoraria)
                   .HasMaxLength(10)
                   .HasColumnType("varchar(10)")
                   .HasColumnName("CargaHoraria");

            builder.Property(a => a.Professor)
                   .HasMaxLength(40)
                   .HasColumnType("varchar(40)")
                   .HasColumnName("Professor");

            builder.Property(a => a.Cargo)
                   .HasMaxLength(30)
                   .HasColumnType("varchar(30)")
                   .HasColumnName("Cargo");

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

            builder.ToTable("Disciplinas");
        }
    }
}
