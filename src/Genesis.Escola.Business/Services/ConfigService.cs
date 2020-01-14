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
    public class ConfigService : BaseService, IConfigService
    {
        #region Variaveis
        private readonly IConfigRepositorio _configRepositorio;
        #endregion

        #region Construtor
        public ConfigService(IConfigRepositorio configRepositorio,
                                INotificador notificador) : base(notificador)
        {
            _configRepositorio = configRepositorio;
        }
        #endregion

        #region Adicionar
        public async Task<bool> Adicionar(Config config)
        {
            if (!ExecutarValidacao(new ConfigValidacao(), config)) return false;
            await _configRepositorio.Adicionar(config);
            return true;
        }
        #endregion

        #region Atualizar
        public async Task<bool> Atualizar(Config config)
        {
            if (!ExecutarValidacao(new ConfigValidacao(), config)) return false;
            await _configRepositorio.Atualizar(config);
            return true;
        }
        #endregion

        #region Excluir
        public async Task<bool> Remover(Guid id)
        {
            await _configRepositorio.Remover(id);
            return true;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            _configRepositorio.Dispose();
        }
        #endregion
    }
}
