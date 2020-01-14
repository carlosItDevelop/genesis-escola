using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ICronogramaRepositorio : IRepositorioGenerico<Cronograma>
    {
        Task<IEnumerable<Cronograma>> PegarPorPeriodo(DateTime inicio, DateTime fim);
        Task<IEnumerable<Cronograma>> PegarPorDataFinal(DateTime fim);
        Task<IEnumerable<Cronograma>> PegarUltimosDias(int dias);
        Task<IEnumerable<Cronograma>> PegarAtivas();
        Task<IEnumerable<Cronograma>> PegarAtivas(string turma);
    }
}
