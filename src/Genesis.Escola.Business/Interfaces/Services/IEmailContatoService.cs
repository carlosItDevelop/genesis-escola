using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IEmailContatoService : IDisposable
    {
        Task<bool> Adicionar(EmailContato emailContato);
        Task<bool> Atualizar(EmailContato emailContato);
        Task<bool> Remover(Guid id);
    }
}
