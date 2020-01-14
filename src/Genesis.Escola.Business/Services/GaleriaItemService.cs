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
    public class GaleriaItemService : BaseService, IGaleriaItensService
    {
        #region Variaveis
        private readonly IGaleriaItensRepositorio _galeriaItensRepositorio;
        #endregion

        #region Construtor
        public GaleriaItemService(IGaleriaItensRepositorio galeriaItensRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _galeriaItensRepositorio = galeriaItensRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(GaleriaItens galeria)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new GaleriaItensValidacao(), galeria)) return false;
            await _galeriaItensRepositorio.Adicionar(galeria);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(GaleriaItens galeria)
        {
            if (!ExecutarValidacao(new GaleriaItensValidacao(), galeria)) return false;
            await _galeriaItensRepositorio.Atualizar(galeria);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _galeriaItensRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _galeriaItensRepositorio.Dispose();
        }
        #endregion
    }
}
