using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class CursoAcadescMap : IEntityTypeConfiguration<CursoAcadesc>
    {
        public void Configure(EntityTypeBuilder<CursoAcadesc> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
            //  .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Ciclo)
                .HasMaxLength(3)
                .HasColumnType("varchar(3)")
                .HasColumnName("Ciclo");

            builder.Property(a => a.Codigo)
                .HasMaxLength(3)
                .HasColumnType("varchar(3)")
                .HasColumnName("Codigo");

            builder.Property(a => a.Nome)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("Nome");

            builder.Property(a => a.NumSeries)
                    .HasColumnType("int")
                    .HasColumnName("NumSeries");

            builder.ToTable("CursoAcadesc");
        }
    }
}
