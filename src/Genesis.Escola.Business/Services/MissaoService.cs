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
    public class MissaoService : BaseService, IMissaoService
    {

        #region Variaveis
        private readonly IMissaoRepositorio _missaoRepositorio;
        #endregion

        #region Construtor
        public MissaoService(IMissaoRepositorio missaoRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _missaoRepositorio = missaoRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Missao missao)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new MissaoValidacao(), missao)) return false;
            await _missaoRepositorio.Adicionar(missao);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Missao missao)
        {
            if (!ExecutarValidacao(new MissaoValidacao(), missao)) return false;
            await _missaoRepositorio.Atualizar(missao);
            return true;
        }
        #endregion

        #region Remover
        public async Task<bool> Remover(Guid id)
        {
            await _missaoRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _missaoRepositorio.Dispose();
        }
        #endregion
    }
}
