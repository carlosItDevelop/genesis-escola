using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class GaleriaMap : IEntityTypeConfiguration<Galeria>
    {
        public void Configure(EntityTypeBuilder<Galeria> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
              //  .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Titulo)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasColumnName("Titulo");

            builder.Property(a => a.CaminhoImagem)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem");

            builder.Property(a => a.Descricao)
                .HasMaxLength(400)
                .IsRequired()
                .HasColumnType("varchar(400)")
                .HasColumnName("Descricao");

            builder.HasMany(a => a.galeriaItens)
                .WithOne(a => a.Galeria)
                .HasForeignKey(a => a.GaleriaId)
                .HasPrincipalKey(a => a.Id);

            builder.ToTable("Galeria");
        }
    }
}
