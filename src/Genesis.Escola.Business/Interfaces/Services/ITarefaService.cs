using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ITarefaService : IDisposable
    {
        Task<bool> Adicionar(Tarefa tarefa);
        Task<bool> Atualizar(Tarefa tarefa);
        Task<bool> Remover(Guid id);
    }
}
