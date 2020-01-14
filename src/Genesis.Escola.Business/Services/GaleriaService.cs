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
    public class GaleriaService : BaseService, IGaleriaService
    {
        #region Variaveis
        private readonly IGaleriaRepositorio _galeriaRepositorio;
        #endregion

        #region Construtor
        public GaleriaService(IGaleriaRepositorio galeriaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _galeriaRepositorio= galeriaRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Galeria galeria)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new GaleriaValidacao(), galeria)) return false;
            await _galeriaRepositorio.Adicionar(galeria);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Galeria galeria)
        {
            if (!ExecutarValidacao(new GaleriaValidacao(), galeria)) return false;
            await _galeriaRepositorio.Atualizar(galeria);
            return true;
        }
        #endregion

        #region Remover
        public async Task<bool> Remover(Guid id)
        {
            await _galeriaRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _galeriaRepositorio.Dispose();
        }
        #endregion
    }
}
