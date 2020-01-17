using AutoMapper;
using Genesis.Escola.Api.Controllers;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Models;
using Genesis.Escola.Business.Interfaces.Services;
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
    public class TurmaAcadescController : MainController
    {
        #region Variaveis
        private readonly ITurmaAcadescRepositorio _turmaRepositorio;
        private readonly IMapper _mapper;
        private readonly ITurmaAcadescService _turmaService;
        #endregion

        #region Construtor
        public TurmaAcadescController(ITurmaAcadescRepositorio turmaRepositorio,
                               IMapper mapper,
                               ITurmaAcadescService turmaService, INotificador notificador,
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
        public async Task<IEnumerable<TurmaAcadescViewModel>> ObterTodos()
        {
            var entidade = _mapper.Map<IEnumerable<TurmaAcadescViewModel>>(await _turmaRepositorio.ObterTodos());
            return entidade;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<TurmaAcadescViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<TurmaAcadescViewModel>(await _turmaRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("{serie}/{turma}/{turno}")]
        [AllowAnonymous]
        public async Task<ActionResult<TurmaAcadescViewModel>> Obter(string serie, string turma, string turno)
        {
            var entidade = _mapper.Map<TurmaAcadescViewModel>(await _turmaRepositorio.TurmaExiste(serie, turma, turno));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("{ciclo}")]
        [AllowAnonymous]
        public async Task<IEnumerable<TurmaAcadescViewModel>> Obter(string ciclo)
        {
            var entidade = _mapper.Map <IEnumerable<TurmaAcadescViewModel>> (await _turmaRepositorio.PegarTurmasCiclo(ciclo));
            return entidade;
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<TurmaAcadescViewModel>> Adicionar(TurmaAcadescViewModel turmaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _turmaService.Adicionar(_mapper.Map<TurmaAcadesc>(turmaViewModel));
            return CustomResponse(turmaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TurmaAcadescViewModel>> Atualizar(Guid id, TurmaAcadescViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(turmaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _turmaService.Atualizar(_mapper.Map<TurmaAcadesc>(turmaViewModel));
            return CustomResponse(turmaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<TurmaAcadescViewModel>> Excluir(Guid id)
        {
            var turmaViewModel = await _turmaRepositorio.ObterPorId(id);
            if (turmaViewModel == null) return NotFound();
            await _turmaService.Remover(id);
            return CustomResponse(turmaViewModel);
        }
        #endregion
    }
}