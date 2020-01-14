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
    public class SobreService : BaseService, ISobreService
    {
        #region Variaveis
        private readonly ISobreRepositorio _sobreRepositorio;
        #endregion

        #region Construtor
        public SobreService(ISobreRepositorio sobreRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _sobreRepositorio = sobreRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Sobre sobre)
        {
            if (!ExecutarValidacao(new SobreValidacao(), sobre)) return false;
            await _sobreRepositorio.Adicionar(sobre);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Sobre sobre)
        {
            if (!ExecutarValidacao(new SobreValidacao(), sobre)) return false;
            await _sobreRepositorio.Atualizar(sobre);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _sobreRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _sobreRepositorio.Dispose();
        }
        #endregion
    }
}
