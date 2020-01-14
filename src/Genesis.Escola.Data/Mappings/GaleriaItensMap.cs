using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class GaleriaItensMap : IEntityTypeConfiguration<GaleriaItens>
    {
        public void Configure(EntityTypeBuilder<GaleriaItens> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
            //    .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Titulo)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasColumnName("Titulo");

            builder.Property(a => a.CaminhoImagem)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem");

            builder.Property(a => a.Resumo)
                   .HasMaxLength(255)
                   .HasColumnType("varchar(255)")
                   .HasColumnName("Resumo");

            builder.HasOne(a => a.Galeria)
                   .WithMany(a => a.galeriaItens)
                   .HasForeignKey(a => a.GaleriaId)
                   .HasPrincipalKey(a => a.Id);

            builder.ToTable("GaleriaItem");
        }
    }
}
