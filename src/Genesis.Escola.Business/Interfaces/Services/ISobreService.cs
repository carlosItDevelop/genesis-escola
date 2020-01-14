using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ISobreService : IDisposable
    {
        Task<bool> Adicionar(Sobre sobre);
        Task<bool> Atualizar(Sobre sobre);
        Task<bool> Remover(Guid id);
    }
}
