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
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ComunicadoController : MainController
    {
        #region Variaveis
        private readonly IComunicadoRepositorio _comunicadoRepositorio;
        private readonly IMapper _mapper;
        private readonly IComunicadoService _comunicadoService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public ComunicadoController(IComunicadoRepositorio noticiaRepositorio,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IHostingEnvironment env,
                                  IComunicadoService noticiaService,
                                  IUser user) : base(notificador, user)
        {
            _comunicadoRepositorio = noticiaRepositorio;
            _env = env;
            _mapper = mapper;
            _comunicadoService = noticiaService;

        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<ComunicadoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ComunicadoViewModel>>(await _comunicadoRepositorio.ObterTodos()); ;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ComunicadoViewModel>> ObterPorId(Guid id)
        {
            var comunicado = _mapper.Map<ComunicadoViewModel>(await _comunicadoRepositorio.ObterPorId(id));
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
        public async Task<IEnumerable<Comunicado>> ObterPorDataFinal(DateTime datafinal)
        {
            return _mapper.Map<IEnumerable<Comunicado>>(await _comunicadoRepositorio.PegarPorDataFinal(datafinal));
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
        public async Task<IEnumerable<Comunicado>> ObterAtivos()
        {
            return _mapper.Map<IEnumerable<Comunicado>>(await _comunicadoRepositorio.PegarAtivas());
        }

        // GET: api/Comunicados
        /// <summary>
        /// retorna Comunicados dentro da validade de data
        /// </summary>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Ocorreu tudo certo </response>
        [Route("ObterAtivos/{turma}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Comunicado>> ObterAtivos(string turma)
        {
            return _mapper.Map<IEnumerable<Comunicado>>(await _comunicadoRepositorio.PegarAtivas(turma));
        }
        #endregion

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<ComunicadoViewModel>> Adicionar(ComunicadoViewModel comunicadoViewModel)
        {
            if (comunicadoViewModel.DescricaoCompleta==null && comunicadoViewModel.ImagemUpload==null)
            {
                ModelState.AddModelError(string.Empty, "Obrigatório ter a Descrição Completa ou o Arquivo PDF ");
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            comunicadoViewModel.Id = Guid.NewGuid();
            if (comunicadoViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Comunicados/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(comunicadoViewModel.ImagemUpload, comunicadoViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                comunicadoViewModel.CaminhoImagem = gravaPdf.Value;
            }

            var result = await _comunicadoService.Adicionar(_mapper.Map<Comunicado>(comunicadoViewModel));
            return CustomResponse(comunicadoViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ComunicadoViewModel>> Atualizar(Guid id, ComunicadoViewModel comunicadoViewModel)
        {
            if (id != comunicadoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(comunicadoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (comunicadoViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Pdf/Comunicados/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaPdf = Pdfs.UploadArquivo(comunicadoViewModel.ImagemUpload, comunicadoViewModel.Id.ToString(), caminho, caminhoAmbiente, false);
                if (gravaPdf.Key == 1)
                {
                    return CustomResponse(gravaPdf.Value);
                }
                comunicadoViewModel.CaminhoImagem = gravaPdf.Value;
            }
            await _comunicadoService.Atualizar(_mapper.Map<Comunicado>(comunicadoViewModel));
            return CustomResponse(comunicadoViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<ComunicadoViewModel>> Excluir(Guid id)
        {
            var comunicadoViewModel = await _comunicadoRepositorio.ObterPorId(id);
            if (comunicadoViewModel == null) return NotFound();
            await _comunicadoService.Remover(id);
            var caminho = "/Paginas/Pdf/Comunicados/";
            var caminhoAmbiente = _env.WebRootPath +  @comunicadoViewModel.CaminhoImagem;
            if (!string.IsNullOrEmpty(comunicadoViewModel.CaminhoImagem)) System.IO.File.Delete(caminhoAmbiente);
            return CustomResponse(comunicadoViewModel);
        }
        #endregion

        #region Pegar PDF do Servidor
        [AcceptVerbs("GET", "POST")]
        [Route("PegarPdf/{id:Guid}")]
        //[Route("PegarPdf/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PdfGetAsync(Guid Id)
        {
            var comunicado = _mapper.Map<ComunicadoViewModel>(await _comunicadoRepositorio.ObterPorId(Id));
            if (!string.IsNullOrEmpty(comunicado.CaminhoImagem))
            {
                var webRoot = _env.WebRootPath + comunicado.CaminhoImagem;
                var ext = Path.GetExtension(webRoot);
                var contents = System.IO.File.ReadAllBytes(webRoot);
                return File(contents, "application/pdf");
            }
            return null;
        }
        #endregion
    }
}