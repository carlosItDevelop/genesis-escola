using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class SobreMap : IEntityTypeConfiguration<Sobre>
    {
        public void Configure(EntityTypeBuilder<Sobre> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
             //   .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.SobreResumido)
                .HasMaxLength(800)
                .IsRequired()
                .HasColumnType("varchar(800)")
                .HasColumnName("DescricaoResumida");

            builder.Property(a => a.SobreCompleto)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("SobreCompleto");

            builder.Property(a => a.CaminhoImagemPrincipal)
                   .HasMaxLength(255)
                   .IsRequired()
                   .HasColumnType("varchar(255)")
                   .HasColumnName("CaminhoImagemPrincipal");

            builder.ToTable("Sobre");
        }
    }
}
