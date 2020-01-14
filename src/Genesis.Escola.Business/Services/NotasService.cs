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
    public class NotasService : BaseService, INotasService
    {
        #region Variaveis
        private readonly INotasRepositorio _notasRepositorio;
        #endregion

        #region Construtor
        public NotasService(INotasRepositorio notasRepositorio,
                            INotificador notificador) : base(notificador)
        {
            _notasRepositorio = notasRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Notas nota)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new NotasValidacao(), nota)) return false;
            await _notasRepositorio.Adicionar(nota);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Notas nota)
        {
            if (!ExecutarValidacao(new NotasValidacao(), nota)) return false;
            await _notasRepositorio.Atualizar(nota);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _notasRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _notasRepositorio.Dispose();
        }
        #endregion
    }
}
