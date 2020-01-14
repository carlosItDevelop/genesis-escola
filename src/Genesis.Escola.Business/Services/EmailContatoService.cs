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
    public class EmailContatoService : BaseService, IEmailContatoService
    {
        private readonly IEmailContatoRepositorio _emailContatoRepositorio;
        public EmailContatoService(IEmailContatoRepositorio emailContatoRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _emailContatoRepositorio = emailContatoRepositorio;
        }

        public async Task<bool> Adicionar(EmailContato emailContato)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new EmailContatoValidacao(), emailContato)) return false;

            await _emailContatoRepositorio.Adicionar(emailContato);
            return true;
        }

        public async Task<bool> Atualizar(EmailContato emailContato)
        {
            if (!ExecutarValidacao(new EmailContatoValidacao(), emailContato)) return false;

            await _emailContatoRepositorio.Atualizar(emailContato);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _emailContatoRepositorio.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _emailContatoRepositorio.Dispose();
        }
    }
}
