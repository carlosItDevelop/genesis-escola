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

namespace Genesis.Escola.Data.Repository
{
    public class TarefaRepositorio : RepositorioGenerico<Tarefa>, ITarefaRepositorio
    {
        public TarefaRepositorio(Contexto contexto) : base(contexto) { }

        public async Task<IEnumerable<Tarefa>> PegarPorPeriodo(DateTime inicio, DateTime fim)
        {
            return await Db.Tarefas.Where(n => n.DataInicio >= inicio && n.DataFinal <= fim)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> PegarPorDataFinal(DateTime fim)
        {
            IEnumerable<Tarefa> Tarefa = await Db.Tarefas.Where(n => n.DataFinal >= DateTime.Today && n.DataFinal.Value <= fim)
                                             .Select(n => n).ToListAsync();
            return Tarefa.Where(p => p.DataInicio.Value <= DateTime.Today);
        }

        public async Task<IEnumerable<Tarefa>> PegarAtivas()
        {
            return await Db.Tarefas.Where(n => n.DataInicio <= DateTime.Today && n.DataFinal.Value >= DateTime.Today)
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> PegarAtivas(string turma)
        {
            return await Db.Tarefas.Where(n => n.DataInicio <= DateTime.Today && n.DataFinal.Value >= DateTime.Today && n.TurmaId.Contains(turma))
                                             .Select(n => n).ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> PegarUltimosDias(int dias)
        {
            return await PegarPorPeriodo(DateTime.Today, DateTime.Today.AddDays(dias));
        }

    }
}
