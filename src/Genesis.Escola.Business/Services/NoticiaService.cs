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
    public class NoticiaService : BaseService, INoticiaService
    {
        private readonly INoticiaRepositorio _noticiaRepositorio;
        public NoticiaService(INoticiaRepositorio noticiaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _noticiaRepositorio = noticiaRepositorio;
        }

        public async Task<bool> Adicionar(Noticia noticia)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new NoticiaValidacao(), noticia)) return false;

            await _noticiaRepositorio.Adicionar(noticia);
            return true;
        }

        public async Task<bool> Atualizar(Noticia noticia)
        {
            if (!ExecutarValidacao(new NoticiaValidacao(), noticia)) return false;

            await _noticiaRepositorio.Atualizar(noticia);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            await _noticiaRepositorio.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _noticiaRepositorio.Dispose();
        }
    }
}
