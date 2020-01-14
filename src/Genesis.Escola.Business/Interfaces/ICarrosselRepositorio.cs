using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ICarrosselRepositorio : IRepositorioGenerico<Carrossel>
    {
        //Task<IEnumerable<Carrossel>> PegarPorDataFinal(DateTime fim);
    }
}
