using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;

namespace Genesis.Escola.Data.Repository
{
    public class CarrosselRepositorio : RepositorioGenerico<Carrossel>, ICarrosselRepositorio
    {
        public CarrosselRepositorio(Contexto contexto) : base(contexto) { }

        //public async Task<IEnumerable<Carrossel>> PegarPorDataFinal(DateTime fim)
        //{
        //    IEnumerable<Carrossel> carrossel = await Db.Carrossel.Where(n => n.DataFinal >= DateTime.Now && n.DataFinal.Value <= fim)
        //                         .Select(n => n).ToListAsync();
        //    return carrossel.Where(p => p.DataInicio.Value <= DateTime.Now);
        //}
    }
}
