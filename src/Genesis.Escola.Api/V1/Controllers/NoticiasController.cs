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
    public class NoticiasController : MainController
    {

        #region Variaveis
        private readonly INoticiaRepositorio _noticiaRepositorio;
        private readonly IMapper _mapper;
        private readonly INoticiaService _noticiaService;
        #endregion

        #region Construtor
        public NoticiasController(INoticiaRepositorio noticiaRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  INoticiaService noticiaService,
                                  IUser user) : base(notificador, user)
        {
            _noticiaRepositorio = noticiaRepositorio;
            _mapper = mapper;
            _noticiaService = noticiaService;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<NoticiaViewModel>> ObterTodos()
        {
            var noticia = _mapper.Map<IEnumerable<NoticiaViewModel>>(await _noticiaRepositorio.ObterTodos());
            return noticia;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<NoticiaViewModel>> ObterPorId(Guid id)
        {
            var noticia = _mapper.Map<NoticiaViewModel>(await _noticiaRepositorio.ObterPorId(id));
            if (noticia == null) return NotFound();
            return Ok(noticia);
        }

        #region Obter pela data final
        // GET: api/Noticia
        /// <summary>
        /// retorna noticias pela data final e retornando com data inicial menor que hoje
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterPorDataFinal/{datafinal}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Noticia>> ObterPorDataFinal(DateTime datafinal)
        {
            return _mapper.Map<IEnumerable<Noticia>>(await _noticiaRepositorio.PegarPorDataFinal(datafinal));
            //return _noticiaRepositorio.PegarPorDataFinal(datafinal);
        }
        #endregion

        #region Obter Niticias Ativas
        // GET: api/Noticia
        /// <summary>
        /// retorna noticias dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterAtivas")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Noticia>> ObterAtivas()
        {
            return _mapper.Map<IEnumerable<Noticia>>(await _noticiaRepositorio.PegarAtivas());
        }
        #endregion

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<NoticiaViewModel>> Adicionar(NoticiaViewModel noticiaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _noticiaService.Adicionar(_mapper.Map<Noticia>(noticiaViewModel));
            return CustomResponse(noticiaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<NoticiaViewModel>> Atualizar(Guid id, NoticiaViewModel noticiaViewModel)
        {
            if (id != noticiaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(noticiaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _noticiaService.Atualizar(_mapper.Map<Noticia>(noticiaViewModel));
            return CustomResponse(noticiaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<NoticiaViewModel>> Excluir(Guid id)
        {
            var noticiaViewModel = await _noticiaRepositorio.ObterPorId(id);
            if (noticiaViewModel == null) return NotFound();
            await _noticiaService.Remover(id);
            return CustomResponse(noticiaViewModel);
        }
        #endregion

        
    }
}
