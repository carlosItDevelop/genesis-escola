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
    public class FilosofiaService : BaseService, IFilosofiaService
    {
        #region Variaveis
        private readonly IFilosofiaRepositorio _filosofiaRepositorio;
        #endregion

        #region Construtor
        public FilosofiaService(IFilosofiaRepositorio filosofiaRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _filosofiaRepositorio = filosofiaRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Filosofia filosofia)
        {
            // Validar o Estado da Entidade
            if (!ExecutarValidacao(new FilosofiaValidacao(), filosofia)) return false;
            await _filosofiaRepositorio.Adicionar(filosofia);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Filosofia filosofia)
        {
            if (!ExecutarValidacao(new FilosofiaValidacao(), filosofia)) return false;
            await _filosofiaRepositorio.Atualizar(filosofia);
            return true;
        }
        #endregion

        #region Remover
        public async Task<bool> Remover(Guid id)
        {
            await _filosofiaRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _filosofiaRepositorio.Dispose();
        }
        #endregion
    }
}
