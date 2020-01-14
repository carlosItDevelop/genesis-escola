using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface IComunicadoRepositorio : IRepositorioGenerico<Comunicado>
    {
        Task<IEnumerable<Comunicado>> PegarPorPeriodo(DateTime inicio, DateTime fim);
        Task<IEnumerable<Comunicado>> PegarPorDataFinal(DateTime fim);
        Task<IEnumerable<Comunicado>> PegarUltimosDias(int dias);
        Task<IEnumerable<Comunicado>> PegarAtivas();
        Task<IEnumerable<Comunicado>> PegarAtivas(string turma);
    }
}
