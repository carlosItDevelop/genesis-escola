using AutoMapper;
using Genesis.Escola.Api.Controllers;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class MissaoController : MainController
    {
        #region Variaveis
        private readonly IMissaoRepositorio _missaoRepositorio;
        private readonly IMapper _mapper;
        private readonly IMissaoService _missaoService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public MissaoController(IMissaoRepositorio missaoRepositorio,
                               IMapper mapper,
                               IMissaoService missaoService,
                               IHostingEnvironment env,
                               INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _missaoRepositorio = missaoRepositorio;
            _mapper = mapper;
            _missaoService = missaoService;
            _env = env;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<MissaoViewModel> ObterTodos()
        {
            return _mapper.Map<MissaoViewModel>(await _missaoRepositorio.PegarDados());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<MissaoViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<MissaoViewModel>(await _missaoRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<MissaoViewModel>> Adicionar(MissaoViewModel missaoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _missaoService.Adicionar(_mapper.Map<Missao>(missaoViewModel));
            return CustomResponse(missaoViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<MissaoViewModel>> Atualizar(Guid id, MissaoViewModel missaoViewModel)
        {
            if (id != missaoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(missaoViewModel);
            }
            await _missaoService.Atualizar(_mapper.Map<Missao>(missaoViewModel));
            return CustomResponse(missaoViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PoloViewModel>> Excluir(Guid id)
        {
            var missaoViewModel = await _missaoRepositorio.ObterPorId(id);
            if (missaoViewModel == null) return NotFound();
            await _missaoService.Remover(id);
            return CustomResponse(missaoViewModel);
        }
        #endregion
    }
}