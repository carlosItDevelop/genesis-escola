using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IDisciplinasService : IDisposable
    {
        Task<bool> Adicionar(Disciplinas disciplina);
        Task<bool> Atualizar(Disciplinas disciplina);
        Task<bool> Remover(Guid id);
    }
}
