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
using static Genesis.Escola.Business.Enumeradores.Enumeradores;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CursosController : MainController
    {
        #region Variaveis
        private readonly ICursosRepositorio _cursoRepositorio;
        private readonly IMapper _mapper;
        private readonly ICursoService _cursoService;
        #endregion

        #region Construtor
        public CursosController(ICursosRepositorio cursosRepositorio,
                               IMapper mapper,
                               ICursoService cursoService, INotificador notificador, 
                               IUser user) : base(notificador, user)
        {
            _cursoRepositorio = cursosRepositorio;
            _mapper = mapper;
            _cursoService = cursoService;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<CursosViewModel> ObterTodos(EnumCurso curso)
        {
            return _mapper.Map<CursosViewModel>(await _cursoRepositorio.PegarDados(curso));
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<CursosViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<CursosViewModel>(await _cursoRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<CursosViewModel>> Adicionar(CursosViewModel cursosViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _cursoService.Adicionar(_mapper.Map<Cursos>(cursosViewModel));
            return CustomResponse(cursosViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CursosViewModel>> Atualizar(Guid id, CursosViewModel cursosViewModel)
        {
            if (id != cursosViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(cursosViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _cursoService.Atualizar(_mapper.Map<Cursos>(cursosViewModel));
            return CustomResponse(cursosViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CursosViewModel>> Excluir(Guid id)
        {
            var cursoViewModel = await _cursoRepositorio.ObterPorId(id);
            if (cursoViewModel == null) return NotFound();
            await _cursoService.Remover(id);
            return CustomResponse(cursoViewModel);
        }
        #endregion
    }
}
