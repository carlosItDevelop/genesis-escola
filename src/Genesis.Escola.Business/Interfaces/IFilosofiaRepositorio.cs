﻿using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface IFilosofiaRepositorio : IRepositorioGenerico<Filosofia>
    {
        Task<Filosofia> PegarDados();
    }
}
