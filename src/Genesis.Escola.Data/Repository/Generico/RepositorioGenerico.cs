using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models.Base;
using Genesis.Escola.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository.Generico
{

    public abstract class RepositorioGenerico<TEntity> : IDisposable, IRepositorioGenerico<TEntity> where TEntity : Entity, new()
    {
        #region Variaveis
        protected readonly Contexto Db;
        protected readonly DbSet<TEntity> DbSet;
        #endregion

        #region Construtor
        protected RepositorioGenerico(Contexto db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        #endregion

        #region Metodos
        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await Salvar();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await Salvar();
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = await ObterPorId(id);
            //DbSet.Remove(new TEntity { Id = id });
            DbSet.Remove(entity);
            await Salvar();
        }

        public async Task<int> Salvar()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
        #endregion
    }
}
