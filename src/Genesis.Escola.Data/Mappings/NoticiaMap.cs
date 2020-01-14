using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class NoticiaMap : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
                //.HasColumnType("UniqueIdentifier");

            builder.Property(a => a.DescricaoResumida)
                .HasMaxLength(600)
                .IsRequired()
                .HasColumnType("varchar(600)")
                .HasColumnName("DescricaoResumida");

            builder.Property(a => a.DescricaoCompleta)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("DescricaoCompleta");

            builder.Property(a => a.DataInicio)
            .HasColumnName("DataInicio")
            .HasColumnType("date");

            builder.Property(a => a.DataFinal)
                   .HasColumnName("DataFinal")
                   .HasColumnType("date");

            builder.ToTable("Noticias");

        }
    }
}
