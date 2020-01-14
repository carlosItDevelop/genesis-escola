using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ITurmaService
    {
        Task<bool> Adicionar(Turma turma);
        Task<bool> Atualizar(Turma turma);
        Task<bool> Remover(Guid id);
    }
}
