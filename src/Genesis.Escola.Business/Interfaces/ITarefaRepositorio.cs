using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ITarefaRepositorio : IRepositorioGenerico<Tarefa>
    {
        Task<IEnumerable<Tarefa>> PegarPorPeriodo(DateTime inicio, DateTime fim);
        Task<IEnumerable<Tarefa>> PegarPorDataFinal(DateTime fim);
        Task<IEnumerable<Tarefa>> PegarUltimosDias(int dias);
        Task<IEnumerable<Tarefa>> PegarAtivas();
        Task<IEnumerable<Tarefa>> PegarAtivas(string turma);
    }
}
