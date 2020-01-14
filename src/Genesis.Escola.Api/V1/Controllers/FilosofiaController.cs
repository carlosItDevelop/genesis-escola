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
    public class FilosofiaController : MainController
    {
        #region Variaveis
        private readonly IFilosofiaRepositorio _filosofiaRepositorio;
        private readonly IMapper _mapper;
        private readonly IFilosofiaService _filosofiaService;
        #endregion

        #region Construtor
        public FilosofiaController(IFilosofiaRepositorio filosofiaRepositorio,
                               IMapper mapper,
                               IFilosofiaService filosofiaService, 
                               INotificador notificador, 
                               IUser user) : base(notificador, user)
        {
            _filosofiaRepositorio = filosofiaRepositorio;
            _mapper = mapper;
            _filosofiaService = filosofiaService;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<FilosofiaViewModel> ObterTodos()
        {
            return _mapper.Map<FilosofiaViewModel>(await _filosofiaRepositorio.PegarDados());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<FilosofiaViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<FilosofiaViewModel>(await _filosofiaRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<FilosofiaViewModel>> Adicionar(FilosofiaViewModel filosofiaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _filosofiaService.Adicionar(_mapper.Map<Filosofia>(filosofiaViewModel));
            return CustomResponse(filosofiaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FilosofiaViewModel>> Atualizar(Guid id, FilosofiaViewModel filosofiaViewModel)
        {
            if (id != filosofiaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(filosofiaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _filosofiaService.Atualizar(_mapper.Map<Filosofia>(filosofiaViewModel));
            return CustomResponse(filosofiaViewModel);
        }
        #endregion
    }
}
