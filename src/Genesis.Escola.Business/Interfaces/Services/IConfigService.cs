using Genesis.Escola.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Interfaces.Services
{
    public interface IConfigService
    {
        Task<bool> Adicionar(Config config);
        Task<bool> Atualizar(Config config);
        Task<bool> Remover(Guid id);
    }
}
