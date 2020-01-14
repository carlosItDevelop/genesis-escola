using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class CarrosselMap : IEntityTypeConfiguration<Carrossel>
    {
        public void Configure(EntityTypeBuilder<Carrossel> builder)
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

            builder.Property(a => a.Resumo)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("varchar(60)")
                .HasColumnName("Resumo");

            builder.Property(a => a.CaminhoImagem)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem");

            //builder.Property(a => a.DataInicio)
            //       .HasColumnName("DataInicio")
            //       .HasColumnType("date");

            //builder.Property(a => a.DataFinal)
            //       .HasColumnName("DataFinal")
            //       .HasColumnType("date");

            builder.Property(a => a.Ativo)
                   .HasMaxLength(1)
                   .HasColumnType("varchar(1)")
                   .HasColumnName("Ativo");

            builder.ToTable("Noticias");

            builder.ToTable("Carrossel");
        }
    }
}
