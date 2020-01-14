using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IGaleriaItensService : IDisposable
    {
        Task<bool> Adicionar(GaleriaItens galeriaItem);
        Task<bool> Atualizar(GaleriaItens galeriaItem);
        Task<bool> Remover(Guid id);
    }
}
