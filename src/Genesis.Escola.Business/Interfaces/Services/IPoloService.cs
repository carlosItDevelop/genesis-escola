using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IPoloService : IDisposable
    {
        Task<bool> Adicionar(Polo polo);
        Task<bool> Atualizar(Polo polo);
        Task<bool> Remover(Guid id);
    }
}
