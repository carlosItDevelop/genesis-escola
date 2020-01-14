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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class NotasController : MainController
    {
        #region Variaveis
        private readonly INotasRepositorio _notaRepositorio;
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IDisciplinasRepositorio _disciplinasRepositorio;
        private readonly IMapper _mapper;
        private readonly INotasService _notaService;
        private readonly IDisciplinasService _disciplinasService;
        private readonly IAlunoService _alunoService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public NotasController(INotasRepositorio notaRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IAlunoRepositorio alunoRepositorio,
                                  IDisciplinasRepositorio disciplinasRepositorio,
                                  IHostingEnvironment env,
                                  INotasService notaService,
                                  IAlunoService alunoService,
                                  IDisciplinasService disciplinasService,
                                  IUser user) : base(notificador, user)
        {
            _notaRepositorio = notaRepositorio;
            _disciplinasRepositorio = disciplinasRepositorio;
            _env = env;
            _mapper = mapper;
            _notaService = notaService;
            _disciplinasService = disciplinasService;
            _alunoRepositorio = alunoRepositorio;
            _alunoService = alunoService;
        }
        #endregion

        #region Get



        #region Obter Comunicados Ativos

        // GET: api/Tarefas
        /// <summary>
        /// retorna Tarefas dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterPorAluno/{aluno}/{senha}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<NotasViewModel>> ObterAtivos(string aluno, string senha)
        {
            var entidade = _mapper.Map<AlunoViewModel>(await _alunoRepositorio.AlunoExiste(aluno));
            var notaslst = new List<NotasViewModel>();
            if (entidade == null)
            {
                NotificarErro("O Aluno ou senha não Existe");
                return notaslst;
            }
            if (entidade.Senha != senha)
            {
                NotificarErro("O Aluno ou senha não Existe");
                return notaslst;
            }

            var retorno = _mapper.Map<IEnumerable<Notas>>(await _notaRepositorio.PegarPorAluno(aluno));
            //var notaslst = new List<NotasViewModel>();
            var notas = new NotasViewModel();
            foreach (var item in retorno)
            {
                notas = new NotasViewModel();

                var retDisc = _mapper.Map<Disciplinas>(_disciplinasRepositorio.PegarPorCodigo(item.Disciplina));
                notas.Materia = retDisc.Nome;
                int valor = 0;
                notas.Nota1b = item.Nota1;
                notas.Nota2b = item.Nota2;
                notas.Nota3b = item.Nota3;
                notas.Nota4b = item.Nota4;
                notas.MediaNota = item.MediaBim;
                notas.RecuperacaoResultFinal = item.NotaRec;
                notas.MediaFinalResultFinal = item.MediaFinal;
                notas.SituacaoResultFinal = item.Situacao;
                int.TryParse(item.Falta1.ToString(), out valor);
                notas.Faltas1b = valor;
                int.TryParse(item.Falta2.ToString(), out valor);
                notas.Faltas2b = valor;
                int.TryParse(item.Falta3.ToString(), out valor);
                notas.Faltas3b = valor;
                int.TryParse(item.Falta4.ToString(), out valor);
                notas.Faltas4b = valor;
                int.TryParse(item.TotalFaltas.ToString(), out valor);
                notas.FaltasTotal = valor;
                notaslst.Add(notas);
            }


            return notaslst;


        }
        #endregion

        #endregion

    }
}