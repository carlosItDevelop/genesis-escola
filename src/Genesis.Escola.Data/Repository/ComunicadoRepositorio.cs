using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository.Generico;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Genesis.Escola.Data.Repository
{
    public class ComunicadoRepositorio : RepositorioGenerico<Comunicado>, IComunicadoRepositorio
    {
        public ComunicadoRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<Comunicado>> PegarPorPeriodo(DateTime inicio, DateTime fim)
        {
            return await Db.Comunicado.Where(n => n.DataInicio >= inicio && n.DataFinal <= fim)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Comunicado>> PegarPorDataFinal(DateTime fim)
        {
            IEnumerable<Comunicado> comunicado = await Db.Comunicado.Where(n => n.DataFinal >= DateTime.Today && n.DataFinal.Value <= fim)
                                             .Select(n => n).ToListAsync();
            return comunicado.Where(p => p.DataInicio.Value <= DateTime.Today);
        }

        public async Task<IEnumerable<Comunicado>> PegarAtivas()
        {
            return await Db.Comunicado.Where(n => n.DataInicio <= DateTime.Today && n.DataFinal.Value >= DateTime.Today)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Comunicado>> PegarAtivas(string turma)
        {
            return await Db.Comunicado.Where(n => n.DataInicio <= DateTime.Today && n.DataFinal.Value >= DateTime.Today && n.TurmaId.Contains(turma))
                                 .Select(n => n).ToListAsync(); 
        }

        public async Task<IEnumerable<Comunicado>> PegarUltimosDias(int dias)
        {
            return await PegarPorPeriodo(DateTime.Today, DateTime.Today.AddDays(dias));
        }

    }
}
