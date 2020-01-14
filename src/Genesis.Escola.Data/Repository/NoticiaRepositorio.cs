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
    public class NoticiaRepositorio : RepositorioGenerico<Noticia>, INoticiaRepositorio
    {
        public NoticiaRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<Noticia>> PegarPorPeriodo(DateTime inicio, DateTime fim)
        {
            return await Db.Noticia.Where(n => n.DataInicio >= inicio && n.DataFinal <= fim)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> PegarPorDataFinal(DateTime fim)
        {
            IEnumerable<Noticia> noticia = await Db.Noticia.Where(n => n.DataFinal >= DateTime.Now && n.DataFinal.Value <= fim)
                                             .Select(n => n).ToListAsync();
            return noticia.Where(p => p.DataInicio.Value <= DateTime.Now);
        }

        public async Task<IEnumerable<Noticia>> PegarAtivas()
        {
            return await Db.Noticia.Where(n => n.DataInicio <= DateTime.Now && n.DataFinal.Value >= DateTime.Now)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Noticia>> PegarUltimosDias(int dias)
        {
            return await PegarPorPeriodo(DateTime.Now, DateTime.Now.AddDays(dias));
        }

    }
}
