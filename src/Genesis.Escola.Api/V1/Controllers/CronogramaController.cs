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
    public class CronogramaController : MainController
    {
        #region Variaveis
        private readonly ICronogramaRepositorio _cronogramaRepositorio;
        private readonly IMapper _mapper;
        private readonly ICronogramaService _cronogramaService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public CronogramaController(ICronogramaRepositorio cronogramaRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IHostingEnvironment env,
                                  ICronogramaService cronogramaService,
                                  IUser user) : base(notificador, user)
        {
            _cronogramaRepositorio = cronogramaRepositorio;
            _env = env;
            _mapper = mapper;
            _cronogramaService = cronogramaService;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CronogramaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CronogramaViewModel>>(await _cronogramaRepositorio.ObterTodos()); ;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<CronogramaViewModel>> ObterPorId(Guid id)
        {
            var comunicado = _mapper.Map<CronogramaViewModel>(await _cronogramaRepositorio.ObterPorId(id));
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
        public async Task<IEnumerable<CronogramaViewModel>> ObterPorDataFinal(DateTime datafinal)
        {
            return _mapper.Map<IEnumerable<CronogramaViewModel>>(await _cronogramaRepositorio.PegarPorDataFinal(datafinal));
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
        public async Task<IEnumerable<CronogramaViewModel>> ObterAtivos()
        {
            return _mapper.Map<IEnumerable<CronogramaViewModel>>(await _cronogramaRepositorio.PegarAtivas());
        }

        // GET: api/Cronograma
        /// <summary>
        /// retorna Cronograma dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterAtivos/{turma}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CronogramaViewModel>> ObterAtivos(string turma)
        {
            return _mapper.Map<IEnumerable<CronogramaViewModel>>(await _cronogramaRepositorio.PegarAtivas(turma));
        }

        #endregion

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<CronogramaViewModel>> Adicionar(CronogramaViewModel cronogramaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            cronogramaViewModel.Id = Guid.NewGuid();
            if (cronogramaViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Cronograma/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(cronogramaViewModel.ImagemUpload, cronogramaViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                cronogramaViewModel.CaminhoImagem = gravaPdf.Value;
            }

            var result = await _cronogramaService.Adicionar(_mapper.Map<Cronograma>(cronogramaViewModel));
            return CustomResponse(cronogramaViewModel);
        }
        #endregion
     
        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CronogramaViewModel>> Atualizar(Guid id, CronogramaViewModel cronogramaViewModel)
        {
            if (id != cronogramaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(cronogramaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (cronogramaViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Cronograma/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(cronogramaViewModel.ImagemUpload, cronogramaViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                cronogramaViewModel.CaminhoImagem = gravaPdf.Value;
            }
            await _cronogramaService.Atualizar(_mapper.Map<Cronograma>(cronogramaViewModel));
            return CustomResponse(cronogramaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<TarefaViewModel>> Excluir(Guid id)
        {
            var cronogramaViewModel = await _cronogramaRepositorio.ObterPorId(id);
            if (cronogramaViewModel == null) return NotFound();
            await _cronogramaService.Remover(id);
            var caminhoAmbiente = _env.WebRootPath + cronogramaViewModel.CaminhoImagem;
            if (!string.IsNullOrEmpty(cronogramaViewModel.CaminhoImagem)) System.IO.File.Delete(caminhoAmbiente);
            return CustomResponse(cronogramaViewModel);
        }
        #endregion

        #region Pegar PDF do Servidor
        [AcceptVerbs("GET", "POST")]
        [Route("PegarPdf/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PdfGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<CronogramaViewModel>(await _cronogramaRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.CaminhoImagem;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "application/pdf");
        }
        #endregion
    }
}