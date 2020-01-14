using Genesis.Escola.Business.Interfaces.Generica;
using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces
{
    public interface ITurmaAcadescRepositorio : IRepositorioGenerico<TurmaAcadesc>
    {
        Task<TurmaAcadesc> TurmaExiste(string serie, string turma, string turno);
    }
}
