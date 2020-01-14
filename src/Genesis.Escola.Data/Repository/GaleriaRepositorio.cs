using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Data.Repository
{
    public class GaleriaRepositorio : RepositorioGenerico<Galeria>, IGaleriaRepositorio
    {
        #region Construtor
        public GaleriaRepositorio(Contexto contexto) : base(contexto) { }
        #endregion

        #region PegarFotos
        public async Task<IEnumerable<Galeria>> PegarFotos(Guid IdGaleria)
        {
            return await Db.Galeria.Where(b => b.Id == IdGaleria).ToListAsync(); ;
        }
        #endregion

    }
}
