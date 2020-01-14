using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ICarrosselService : IDisposable
    {
        Task<bool> Adicionar(Carrossel carrossel);
        Task<bool> Atualizar(Carrossel carrossel);
        Task<bool> Remover(Guid id);
    }
}
