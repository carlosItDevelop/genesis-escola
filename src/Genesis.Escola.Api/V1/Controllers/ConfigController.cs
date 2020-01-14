using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Genesis.Escola.Api.Controllers;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ConfigController : MainController
    {
        #region Variaveis
        private readonly IConfigRepositorio _configRepositorio;
        private readonly IMapper _mapper;
        private readonly IConfigService _configService;
        #endregion

        #region Construtor
        public ConfigController(IConfigRepositorio configRepositorio,
                               IMapper mapper,
                               IConfigService configService, INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _configRepositorio = configRepositorio;
            _mapper = mapper;
            _configService = configService;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<ConfigViewModel> Obter()
        {
            return _mapper.Map<ConfigViewModel>(await _configRepositorio.PegarDados());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConfigViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<ConfigViewModel>(await _configRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<ConfigViewModel>> Adicionar(ConfigViewModel configViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _configService.Adicionar(_mapper.Map<Config>(configViewModel));
            return CustomResponse(configViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ConfigViewModel>> Atualizar(Guid id, ConfigViewModel configViewModel)
        {
            if (id != configViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(configViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _configService.Atualizar(_mapper.Map<Config>(configViewModel));
            return CustomResponse(configViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ConfigViewModel>> Excluir(Guid id)
        {
            var configViewModel = await _configRepositorio.ObterPorId(id);
            if (configViewModel == null) return NotFound();
            await _configService.Remover(id);
            return CustomResponse(configViewModel);
        }
        #endregion
    }
}