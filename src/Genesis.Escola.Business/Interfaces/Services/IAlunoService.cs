using Genesis.Escola.Business.Models;
using System;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IAlunoService : IDisposable
    {
        Task<bool> Adicionar(Aluno aluno);
        Task<bool> Atualizar(Aluno aluno);
        Task<bool> Remover(Guid id);
    }
}
