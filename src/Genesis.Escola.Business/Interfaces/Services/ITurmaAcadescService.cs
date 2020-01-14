using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface ITurmaAcadescService
    {
        Task<bool> Adicionar(TurmaAcadesc turmaAcadesc);
        Task<bool> Atualizar(TurmaAcadesc turmaAcadesc);
        Task<bool> Remover(Guid id);
    }
}
