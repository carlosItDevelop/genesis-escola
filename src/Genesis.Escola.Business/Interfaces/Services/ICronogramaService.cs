using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ICronogramaService : IDisposable
    {
        Task<bool> Adicionar(Cronograma cronograma);
        Task<bool> Atualizar(Cronograma cronograma);
        Task<bool> Remover(Guid id);
    }
}
