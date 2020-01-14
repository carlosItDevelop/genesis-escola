using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class EmailContatoMap : IEntityTypeConfiguration<EmailContato>
    {
        public void Configure(EntityTypeBuilder<EmailContato> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("Id");
                //.HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Nome)
                   .HasMaxLength(80)
                   .HasColumnType("varchar(80)")
                   .HasColumnName("Nome");

            builder.Property(a => a.Email)
                   .HasMaxLength(150)
                   .HasColumnType("varchar(150)")
                   .HasColumnName("Email");

            builder.ToTable("EmailContato");
        }
    }
}
