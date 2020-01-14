using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface INoticiaService : IDisposable
    {
        Task<bool> Adicionar(Noticia noticia);
        Task<bool> Atualizar(Noticia noticia);
        Task<bool> Remover(Guid id);
    }
}
