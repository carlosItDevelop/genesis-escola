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
    //[Authorize]
    public class AlunoController : MainController
    {
        #region Variaveis
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IMapper _mapper;
        private readonly IAlunoService _alunoService;
        #endregion

        #region Construtor
        public AlunoController(IAlunoRepositorio alunoRepositorio,
                               IMapper mapper,
                               IAlunoService alunoService,
                               INotificador notificador,
                               IUser user) :base(notificador,user)
        {
            _alunoRepositorio = alunoRepositorio;
            _mapper = mapper;
            _alunoService = alunoService;
        }
        #endregion

        #region Get

        [HttpGet]
        public async Task<IEnumerable<AlunoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<AlunoViewModel>>(await _alunoRepositorio.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<AlunoViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("{matricula}")]
        public async Task<ActionResult<AlunoViewModel>> ObterAluno(string matricula)
        {
            var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.AlunoExiste(matricula));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("{matricula}/{senha}")]
        public async Task<ActionResult<AlunoViewModel>> ObterAluno(string matricula,string senha)
        {
            var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.AlunoExiste(matricula, senha));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        //[HttpPost]
        //public async Task<ActionResult<AlunoViewModel>> Adicionar(AlunoViewModel alunoViewModel)
        //{
        //    if (!ModelState.IsValid) return CustomResponse(ModelState);
        //    var result = await _alunoService.Adicionar(_mapper.Map<Aluno>(alunoViewModel));
        //    return CustomResponse(alunoViewModel);
        //}
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AlunoViewModel>> Atualizar(Guid id, AlunoViewModel alunoViewModel)
        {
            if (id != alunoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(alunoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _alunoService.Atualizar(_mapper.Map<Aluno>(alunoViewModel));
            return CustomResponse(alunoViewModel);
        }
        #endregion

        #region Delete
        // [HttpDelete("{id:guid}")]
        //public async Task<ActionResult<AlunoViewModel>> Excluir(Guid id)
        //{
        //    var alunoViewModel = await _alunoRepositorio.ObterPorId(id);
        //    if (alunoViewModel == null) return NotFound();
        //    await _alunoService.Remover(id);
        //    return CustomResponse(alunoViewModel);
        //}
        #endregion
    }
}
