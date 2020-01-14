using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ICursoAcadescService : IDisposable
    {
        Task<bool> Adicionar(CursoAcadesc curso);
        Task<bool> Atualizar(CursoAcadesc curso);
        Task<bool> Remover(Guid id);
    }
}
