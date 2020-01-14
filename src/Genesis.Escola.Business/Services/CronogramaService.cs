using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Escola.Business.Services
{
   public class CronogramaService : BaseService, ICronogramaService
    {
        private readonly ICronogramaRepositorio _cronogramaRepositorio;
        public CronogramaService(ICronogramaRepositorio cronogramaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _cronogramaRepositorio = cronogramaRepositorio;
        }

        public async Task<bool> Adicionar(Cronograma cronograma)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new CronogramaValidacao(), cronograma)) return false;

            await _cronogramaRepositorio.Adicionar(cronograma);
            return true;
        }

        public async Task<bool> Atualizar(Cronograma cronograma)
        {
            if (!ExecutarValidacao(new CronogramaValidacao(), cronograma)) return false;

            await _cronogramaRepositorio.Atualizar(cronograma);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _cronogramaRepositorio.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _cronogramaRepositorio.Dispose();
        }
    }
}
