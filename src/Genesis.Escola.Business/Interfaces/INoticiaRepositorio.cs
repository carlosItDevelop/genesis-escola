using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface INoticiaRepositorio : IRepositorioGenerico<Noticia>
    {
        Task<IEnumerable<Noticia>> PegarPorPeriodo(DateTime inicio, DateTime fim);
        Task<IEnumerable<Noticia>> PegarPorDataFinal(DateTime fim);
        Task<IEnumerable<Noticia>> PegarUltimosDias(int dias);
        Task<IEnumerable<Noticia>> PegarAtivas();
    }
}
