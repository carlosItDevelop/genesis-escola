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
    public class CarrosselService : BaseService, ICarrosselService
    {
        #region Variaveis
        private readonly ICarrosselRepositorio _carrosselRepositorio;
        #endregion

        #region Construtor
        public CarrosselService(ICarrosselRepositorio carrosselRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _carrosselRepositorio = carrosselRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Carrossel carrossel)
        {
            if (!ExecutarValidacao(new CarrosselValidacao(), carrossel)) return false;
            await _carrosselRepositorio.Adicionar(carrossel);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Carrossel carrossel)
        {
            if (!ExecutarValidacao(new CarrosselValidacao(), carrossel)) return false;
            await _carrosselRepositorio.Atualizar(carrossel);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _carrosselRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _carrosselRepositorio.Dispose();
        }
        #endregion
    }
}
