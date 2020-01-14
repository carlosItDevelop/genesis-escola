using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IComunicadoService : IDisposable
    {
        Task<bool> Adicionar(Comunicado comunicado);
        Task<bool> Atualizar(Comunicado comunicado);
        Task<bool> Remover(Guid id);
    }
}
