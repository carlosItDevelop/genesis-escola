using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ICursoService : IDisposable
    {
        Task<bool> Adicionar(Cursos curso);
        Task<bool> Atualizar(Cursos curso);
        Task<bool> Remover(Guid id);
    }
}
