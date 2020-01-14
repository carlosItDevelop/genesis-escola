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
    public class EmailContatoController : MainController
    {
        #region Variaveis
        private readonly IEmailContatoRepositorio _emailContatoRepositorio;
        private readonly IMapper _mapper;
        private readonly IEmailContatoService _emailContatoService;
        #endregion

        #region Construtor
        public EmailContatoController(IEmailContatoRepositorio emailContatoRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IEmailContatoService emailContatoService,
                                  IUser user) : base(notificador, user)
        {
            _emailContatoRepositorio = emailContatoRepositorio;
            _mapper = mapper;
            _emailContatoService = emailContatoService;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<EmailContatoViewModel>> ObterTodos()
        {
            var retorno = _mapper.Map<IEnumerable<EmailContatoViewModel>>(await _emailContatoRepositorio.ObterTodos());
            return retorno;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<EmailContatoViewModel>> ObterPorId(Guid id)
        {
            var retorno = _mapper.Map<EmailContatoViewModel>(await _emailContatoRepositorio.ObterPorId(id));
            if (retorno == null) return NotFound();
            return Ok(retorno);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<EmailContatoViewModel>> Adicionar(EmailContatoViewModel entityViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _emailContatoService.Adicionar(_mapper.Map<EmailContato>(entityViewModel));
            return CustomResponse(entityViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EmailContatoViewModel>> Atualizar(Guid id, EmailContatoViewModel entityViewModel)
        {
            if (id != entityViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(entityViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _emailContatoService.Atualizar(_mapper.Map<EmailContato>(entityViewModel));
            return CustomResponse(entityViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<NoticiaViewModel>> Excluir(Guid id)
        {
            var entityViewModel = await _emailContatoRepositorio.ObterPorId(id);
            if (entityViewModel == null) return NotFound();
            await _emailContatoService.Remover(id);
            return CustomResponse(entityViewModel);
        }
        #endregion

    }
}