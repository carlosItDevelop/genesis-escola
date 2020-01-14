using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Genesis.Escola.Api.Controllers;
using Genesis.Escola.Api.Funcoes;
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
    public class TarefaController : MainController
    {
        #region Variaveis
        private readonly ITarefaRepositorio _tarefaRepositorio;
        private readonly IMapper _mapper;
        private readonly ITarefaService _tarefaService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public TarefaController(ITarefaRepositorio tarefaRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IHostingEnvironment env,
                                  ITarefaService tarefaService,
                                  IUser user) : base(notificador, user)
        {
            _tarefaRepositorio = tarefaRepositorio;
            _env = env;
            _mapper = mapper;
            _tarefaService = tarefaService;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TarefaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TarefaViewModel>>(await _tarefaRepositorio.ObterTodos()); ;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<TarefaViewModel>> ObterPorId(Guid id)
        {
            var comunicado = _mapper.Map<TarefaViewModel>(await _tarefaRepositorio.ObterPorId(id));
            if (comunicado == null) return NotFound();
            return Ok(comunicado);
        }

        #region Obter pela data final
        // GET: api/Comunicado
        /// <summary>
        /// retorna COmunicados pela data final e retornando com data inicial menor que hoje
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterPorDataFinal/{datafinal}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TarefaViewModel>> ObterPorDataFinal(DateTime datafinal)
        {
            return _mapper.Map<IEnumerable<TarefaViewModel>>(await _tarefaRepositorio.PegarPorDataFinal(datafinal));
        }
        #endregion

        #region Obter Comunicados Ativos
        // GET: api/Comunicados
        /// <summary>
        /// retorna Comunicados dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterAtivos")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TarefaViewModel>> ObterAtivos()
        {
            return _mapper.Map<IEnumerable<TarefaViewModel>>(await _tarefaRepositorio.PegarAtivas());
        }

        // GET: api/Tarefas
        /// <summary>
        /// retorna Tarefas dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterAtivos/{turma}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<TarefaViewModel>> ObterAtivos(string turma)
        {
            return _mapper.Map<IEnumerable<TarefaViewModel>>(await _tarefaRepositorio.PegarAtivas(turma));
        }
        #endregion

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<TarefaViewModel>> Adicionar(TarefaViewModel tarefaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            tarefaViewModel.Id = Guid.NewGuid();
            if (tarefaViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Tarefas/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(tarefaViewModel.ImagemUpload, tarefaViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                tarefaViewModel.CaminhoImagem = gravaPdf.Value;
            }

            var result = await _tarefaService.Adicionar(_mapper.Map<Tarefa>(tarefaViewModel));
            return CustomResponse(tarefaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TarefaViewModel>> Atualizar(Guid id, TarefaViewModel tarefaViewModel)
        {
            if (id != tarefaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(tarefaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (tarefaViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Tarefas/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(tarefaViewModel.ImagemUpload, tarefaViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                tarefaViewModel.CaminhoImagem = gravaPdf.Value;
            }
            await _tarefaService.Atualizar(_mapper.Map<Tarefa>(tarefaViewModel));
            return CustomResponse(tarefaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<TarefaViewModel>> Excluir(Guid id)
        {
            var tarefaViewModel = await _tarefaRepositorio.ObterPorId(id);
            if (tarefaViewModel == null) return NotFound();
            await _tarefaService.Remover(id);
            var caminhoAmbiente = _env.WebRootPath + tarefaViewModel.CaminhoImagem;
            if (!string.IsNullOrEmpty(tarefaViewModel.CaminhoImagem)) System.IO.File.Delete(caminhoAmbiente);
            return CustomResponse(tarefaViewModel);
        }
        #endregion

        #region Pegar PDF do Servidor
        [AcceptVerbs("GET", "POST")]
        [Route("PegarPdf/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PdfGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<TarefaViewModel>(await _tarefaRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.CaminhoImagem;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "application/pdf");
        }
        #endregion
    }
}