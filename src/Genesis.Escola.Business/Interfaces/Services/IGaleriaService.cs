using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IGaleriaService : IDisposable
    {
        Task<bool> Adicionar(Galeria galeria);
        Task<bool> Atualizar(Galeria galeria);
        Task<bool> Remover(Guid id);
    }
}
