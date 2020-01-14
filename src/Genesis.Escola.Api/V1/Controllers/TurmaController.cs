using AutoMapper;
using Genesis.Escola.Api.Controllers;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class TurmaController : MainController
    {
        #region Variaveis
        private readonly ITurmaRepositorio _turmaRepositorio;
        private readonly IMapper _mapper;
        private readonly ITurmaService _turmaService;
        #endregion

        #region Construtor
        public TurmaController(ITurmaRepositorio turmaRepositorio,
                               IMapper mapper,
                               ITurmaService turmaService, INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _turmaRepositorio = turmaRepositorio;
            _mapper = mapper;
            _turmaService = turmaService;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TurmaViewModel>> ObterTodos()
        {
            var noticia = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepositorio.ObterTodos());
            return noticia;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<TurmaViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<TurmaViewModel>(await _turmaRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<TurmaViewModel>> Adicionar(TurmaViewModel turmaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _turmaService.Adicionar(_mapper.Map<Turma>(turmaViewModel));
            return CustomResponse(turmaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TurmaViewModel>> Atualizar(Guid id, TurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(turmaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _turmaService.Atualizar(_mapper.Map<Turma>(turmaViewModel));
            return CustomResponse(turmaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<TurmaViewModel>> Excluir(Guid id)
        {
            var turmaViewModel = await _turmaRepositorio.ObterPorId(id);
            if (turmaViewModel == null) return NotFound();
            await _turmaService.Remover(id);
            return CustomResponse(turmaViewModel);
        }
        #endregion
    }
}