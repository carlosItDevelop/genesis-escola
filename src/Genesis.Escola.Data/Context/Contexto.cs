using Genesis.Escola.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Genesis.Escola.Data.Context
{
    public class Contexto :DbContext
    {
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes) { }
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Noticia> Noticia { get; set; }
        public DbSet<Sobre> Sobre { get; set; }
        public DbSet<Carrossel> Carrossel { get; set; }
        public DbSet<Polo> Polo { get; set; }
        public DbSet<Filosofia> Filosofia { get; set; }
        public DbSet<Missao> Missao { get; set; }
        public DbSet<Cursos> Cursos { get; set; }
        public DbSet<Galeria> Galeria { get; set; }
        public DbSet<GaleriaItens> GaleriaItens { get; set; }
        public DbSet<Comunicado> Comunicado { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<EmailContato> EmailContato { get; set; }
        public DbSet<TurmaAcadesc> TurmaAcadesc { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Cronograma> Cronogramas { get; set; }
        public DbSet<Notas> Notas { get; set; }
        public DbSet<Disciplinas> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // onde não tiver setado varchar o a propriedade string ficam com varchar(100)
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";


            // Busca os Mapppings de uma vez só
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            //remover delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

    }
}
