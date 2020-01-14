using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IMissaoService : IDisposable
    {
        Task<bool> Adicionar(Missao missao);
        Task<bool> Atualizar(Missao missao);
        Task<bool> Remover(Guid id);
    }
}
