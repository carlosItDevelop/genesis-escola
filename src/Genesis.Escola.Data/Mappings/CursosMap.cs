using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class CursosMap : IEntityTypeConfiguration<Cursos>
    {
        public void Configure(EntityTypeBuilder<Cursos> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
             //   .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Curso)
                .IsRequired()
                .HasColumnName("Curso");

            builder.Property(a => a.Titulo)
                .HasMaxLength(80)
                .IsRequired()
                .HasColumnType("varchar(80)")
                .HasColumnName("Titulo");

            builder.Property(a => a.Descricao)
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("Descricao");

            builder.ToTable("Cursos");
        }
    }
}
