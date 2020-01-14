using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Genesis.Escola.Data.Mappings
{
    public class ConfigMap : IEntityTypeConfiguration<Config>
    {
        public void Configure(EntityTypeBuilder<Config> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
              //  .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.TituloContato)
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasColumnType("varchar(50)")
                   .HasColumnName("TituloContato");

            builder.Property(a => a.DescricaoContato)
                   .HasMaxLength(400)
                   .IsRequired()
                   .HasColumnType("varchar(400)")
                   .HasColumnName("DescricaoContato");

            builder.Property(a => a.TituloTrabalhe)
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasColumnType("varchar(50)")
                   .HasColumnName("TituloTrabalhe");

            builder.Property(a => a.DescricaoTrabalhe)
                   .HasMaxLength(400)
                   .IsRequired()
                   .HasColumnType("varchar(400)")
                   .HasColumnName("DescricaoTrabalhe");

            builder.Property(a => a.Endereco)
                   .HasMaxLength(300)
                   .IsRequired()
                   .HasColumnType("varchar(300)")
                   .HasColumnName("Endereco");

            builder.Property(a => a.Telefones)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnType("varchar(100)")
                   .HasColumnName("Telefones");

            builder.Property(a => a.EmailRetTrabalhe)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnType("varchar(100)")
                   .HasColumnName("EmailRetTrabalhe");

            builder.Property(a => a.EmailEnvio)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnType("varchar(100)")
                   .HasColumnName("EmailEnvio");

            builder.Property(a => a.EmailSenha)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnType("varchar(100)")
                   .HasColumnName("EmailSenha");

            builder.Property(a => a.EmailHost)
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasColumnType("varchar(100)")
                   .HasColumnName("EmailHost");

            builder.Property(a => a.EmailPorta)
                   .IsRequired()
                   .HasColumnType("integer")
                   .HasColumnName("EmailPorta");

            builder.Property(a => a.EmailSsl)
                   .IsRequired()
                   .HasDefaultValue(true)
                   .HasColumnName("EmailSsl");

            builder.Property(a => a.MensagemRetContato)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("MensagemRetContato");

            builder.Property(a => a.MensagemRetTrabalhe)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("MensagemRetTrabalhe");

            builder.ToTable("Config");
        }
    }
}
