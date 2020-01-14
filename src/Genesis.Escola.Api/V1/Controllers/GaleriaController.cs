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
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class GaleriaController : MainController
    {
        #region Variaveis
        private readonly IGaleriaRepositorio _galeriaRepositorio;
        private readonly IMapper _mapper;
        private readonly IGaleriaService _galeriaService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public GaleriaController(IGaleriaRepositorio galeriaRepositorio,
                               IMapper mapper,
                               IGaleriaService galeriaService,
                               IHostingEnvironment env,
                               INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _galeriaRepositorio = galeriaRepositorio;
            _mapper = mapper;
            _galeriaService = galeriaService;
            _env = env;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GaleriaViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GaleriaViewModel>>(await _galeriaRepositorio.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<GaleriaViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<GaleriaViewModel>(await _galeriaRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<GaleriaViewModel>> Adicionar(GaleriaViewModel galeriaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var caminho = "/Paginas/Imagem/Galeria/";
            var caminhoAmbiente = _env.WebRootPath;
            var gravaImagem = Imagens.UploadArquivo(galeriaViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }
            galeriaViewModel.CaminhoImagem = gravaImagem.Value;

            var result = await _galeriaService.Adicionar(_mapper.Map<Galeria>(galeriaViewModel));
            return CustomResponse(galeriaViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GaleriaViewModel>> Atualizar(Guid id, GaleriaViewModel galeriaViewModel)
        {
            if (id != galeriaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(galeriaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (galeriaViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Imagem/Galeria/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaImagem = Imagens.UploadArquivo(galeriaViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho,
                    caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return CustomResponse(gravaImagem.Value);
                }

                galeriaViewModel.CaminhoImagem = gravaImagem.Value;
            }

            await _galeriaService.Atualizar(_mapper.Map<Galeria>(galeriaViewModel));
            return CustomResponse(galeriaViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<GaleriaViewModel>> Excluir(Guid id)
        {
            var galeriaViewModel = await _galeriaRepositorio.ObterPorId(id);
            if (galeriaViewModel == null) return NotFound();
            await _galeriaService.Remover(id);
            return CustomResponse(galeriaViewModel);
        }
        #endregion

        #region Pegar Imagem do Servidor
        [HttpGet]
        [Route("PegarImagem/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> ImageGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<GaleriaViewModel>(await _galeriaRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.CaminhoImagem;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion
    }
}

