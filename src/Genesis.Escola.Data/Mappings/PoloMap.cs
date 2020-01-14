using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class PoloMap : IEntityTypeConfiguration<Polo>
    {
        public void Configure(EntityTypeBuilder<Polo> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
             //   .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.Titulo)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("varchar(60)")
                .HasColumnName("Titulo");

            builder.Property(a => a.DescricaoCurso1)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("DescricaoCurso1");

            builder.Property(a => a.DescricaoCurso2)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("DescricaoCurso2");

            builder.Property(a => a.DescricaoCurso3)
                .HasMaxLength(300)
                .IsRequired()
                .HasColumnType("varchar(300)")
                .HasColumnName("DescricaoCurso3");

            builder.Property(a => a.CaminhoImagem1)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem1");

            builder.Property(a => a.CaminhoImagem2)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem2");

            builder.Property(a => a.CaminhoImagem3)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem3");

            builder.Property(a => a.LinkCurso1)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("LinkCurso1");

            builder.Property(a => a.LinkCurso2)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("LinkCurso2");

            builder.Property(a => a.LinkCurso3)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnType("varchar(255)")
                .HasColumnName("LinkCurso3");

            builder.Property(a => a.NomeCurso1)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasColumnName("NomeCurso1");

            builder.Property(a => a.NomeCurso2)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasColumnName("NomeCurso2");

            builder.Property(a => a.NomeCurso3)
                .HasMaxLength(40)
                .IsRequired()
                .HasColumnType("varchar(40)")
                .HasColumnName("NomeCurso3");

            builder.ToTable("Polo");
        }
    }
}
