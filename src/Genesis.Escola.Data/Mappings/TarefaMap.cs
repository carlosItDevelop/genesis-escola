using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Genesis.Escola.Data.Mappings
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                .HasColumnName("Id");
            //   .HasColumnType("UniqueIdentifier");

            builder.Property(a => a.DescricaoResumida)
                .HasMaxLength(600)
                .IsRequired()
                .HasColumnType("varchar(600)")
                .HasColumnName("DescricaoResumida");

            builder.Property(a => a.DescricaoCompleta)
                .HasColumnType("longtext")
                .HasColumnName("DescricaoCompleta");

            builder.Property(a => a.DataInicio)
            .HasColumnName("DataInicio")
            .HasColumnType("date");

            builder.Property(a => a.DataFinal)
                   .HasColumnName("DataFinal")
                   .HasColumnType("date");

            builder.Property(p => p.TurmaId)
                .HasColumnName("TurmaId")
                .HasMaxLength(7)
                .HasColumnType("varchar(500)")
                .IsRequired();

            builder.Property(p => p.CaminhoImagem)
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("CaminhoImagem");

            builder.ToTable("Tarefas");

        }
    }
}
