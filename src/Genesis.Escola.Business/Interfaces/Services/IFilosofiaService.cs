using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IFilosofiaService : IDisposable
    {
        Task<bool> Adicionar(Filosofia filosofia);
        Task<bool> Atualizar(Filosofia filosofia);
        Task<bool> Remover(Guid id);
    }
}
