using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class TurmaAcadescMap : IEntityTypeConfiguration<TurmaAcadesc>
    {
        public void Configure(EntityTypeBuilder<TurmaAcadesc> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Serie)
                .HasColumnName("Serie")
                .HasMaxLength(1)
                .HasColumnType("varchar(1)")
                .IsRequired();

            builder.Property(p => p.Turma)
                .HasColumnName("Turma")
                .HasMaxLength(5)
                .HasColumnType("varchar(5)")
                .IsRequired();

            builder.Property(p => p.Turno)
                .HasColumnName("Turno")
                .HasMaxLength(1)
                .HasColumnType("varchar(1)")
                .IsRequired();

            builder.Property(p => p.Ciclo)
                .HasColumnName("Ciclo")
                .HasMaxLength(5)
                .HasColumnType("varchar(5)")
                .IsRequired();



            builder.ToTable("TurmaAcadesc");
        }
    }
}
