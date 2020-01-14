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
    public class GaleriaItensRepositorio : RepositorioGenerico<GaleriaItens>, IGaleriaItensRepositorio
    {
        public GaleriaItensRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<GaleriaItens>> PegarFotos(Guid IdGaleria)
        {
            return await Db.GaleriaItens.Where(b => b.GaleriaId == IdGaleria).ToListAsync();

        }
    }
}
