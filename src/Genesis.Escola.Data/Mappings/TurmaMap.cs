using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Sigla)
                .HasColumnName("Sigla")
                .HasMaxLength(10)
                .HasColumnType("varchar(10)")
                .IsRequired();

            //builder.HasMany(a => a.)
            //    .WithOne(a => a.TurmaId)
            //    .HasForeignKey(a => a.TurmaId)
            //    .HasPrincipalKey(a => a.Id);

            builder.ToTable("Turma");
        }
    }
}
