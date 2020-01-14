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
    public class ComunicadoService : BaseService, IComunicadoService
    {
        private readonly IComunicadoRepositorio _comunicadoRepositorio;
        public ComunicadoService(IComunicadoRepositorio comunicadoRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _comunicadoRepositorio = comunicadoRepositorio;
        }

        public async Task<bool> Adicionar(Comunicado comunicado)
        {
            // Validar o Estado da Entidade
            var validar = ExecutarValidacao(new ComunicadoValidacao(), comunicado); 
            if (!ExecutarValidacao(new ComunicadoValidacao(), comunicado)) return false;

            await _comunicadoRepositorio.Adicionar(comunicado);
            return true;
        }

        public async Task<bool> Atualizar(Comunicado comunicado)
        {
            if (!ExecutarValidacao(new ComunicadoValidacao(), comunicado)) return false;

            await _comunicadoRepositorio.Atualizar(comunicado);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _comunicadoRepositorio.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _comunicadoRepositorio.Dispose();
        }
    }
}
