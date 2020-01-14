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
    public class PoloService : BaseService, IPoloService
    {
        #region Variaveis
        private readonly IPoloRepositorio _poloRepositorio;
        #endregion

        #region Construtor
        public PoloService(IPoloRepositorio poloRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _poloRepositorio = poloRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Polo polo)
        {
            if (!ExecutarValidacao(new PoloValidacao(), polo)) return false;
            await _poloRepositorio.Adicionar(polo);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Polo polo)
        {
            if (!ExecutarValidacao(new PoloValidacao(), polo)) return false;
            await _poloRepositorio.Atualizar(polo);
            return true;
        }
        #endregion

        #region Remover
        public async Task<bool> Remover(Guid id)
        {
            await _poloRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _poloRepositorio.Dispose();
        }
        #endregion
    }
}
