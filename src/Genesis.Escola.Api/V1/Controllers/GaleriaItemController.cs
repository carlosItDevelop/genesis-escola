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
using System.Threading.Tasks;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GaleriaItemController : MainController
    {
        #region Variaveis
        private readonly IGaleriaItensRepositorio _galeriaItemRepositorio;
        private readonly IMapper _mapper;
        private readonly IGaleriaItensService _galeriaItemService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public GaleriaItemController(IGaleriaItensRepositorio galeriaItemRepositorio,
                               IMapper mapper,
                               IGaleriaItensService galeriaItemService,
                               IHostingEnvironment env,
                               INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _galeriaItemRepositorio = galeriaItemRepositorio;
            _mapper = mapper;
            _galeriaItemService = galeriaItemService;
            _env = env;
        }
        #endregion

        #region Get

        [HttpGet]
        public async Task<IEnumerable<GaleriaItensViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GaleriaItensViewModel>>(await _galeriaItemRepositorio.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GaleriaItensViewModel>> ObterPorId(Guid id)
        {
            return _mapper.Map<GaleriaItensViewModel>(await _galeriaItemRepositorio.ObterPorId(id));
        }

        [HttpGet("ObterTodosPorGaleria/{id:guid}")]
        public async Task<IEnumerable<GaleriaItensViewModel>> ObterTodosPorGaleria(Guid id)
        {
            return _mapper.Map<IEnumerable<GaleriaItensViewModel>>(await _galeriaItemRepositorio.Buscar(g => g.GaleriaId == id));
        }

        [HttpGet]
        [Route("PegarImagem/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> ImageGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<GaleriaItensViewModel>(await _galeriaItemRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.CaminhoImagem;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<GaleriaItensViewModel>> Adicionar(GaleriaItensViewModel galeriaItensViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var caminho = "/Paginas/Imagem/Galeria/";
            var caminhoAmbiente = _env.WebRootPath;
            var gravaImagem = Imagens.UploadArquivo(galeriaItensViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }
            galeriaItensViewModel.CaminhoImagem = gravaImagem.Value;

            var result = await _galeriaItemService.Adicionar(_mapper.Map<GaleriaItens>(galeriaItensViewModel));
            return CustomResponse(galeriaItensViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GaleriaItensViewModel>> Atualizar(Guid id, GaleriaItensViewModel galeriaItensViewModel)
        {
            if (id != galeriaItensViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(galeriaItensViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (galeriaItensViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Imagem/Galeria/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaImagem = Imagens.UploadArquivo(galeriaItensViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho,
                    caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return CustomResponse(gravaImagem.Value);
                }

                galeriaItensViewModel.CaminhoImagem = gravaImagem.Value;
            }

            await _galeriaItemService.Atualizar(_mapper.Map<GaleriaItens>(galeriaItensViewModel));
            return CustomResponse(galeriaItensViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<GaleriaItensViewModel>> Excluir(Guid id)
        {
            var galeriaItensViewModel = await _galeriaItemRepositorio.ObterPorId(id);
            if (galeriaItensViewModel == null) return NotFound();
            await _galeriaItemService.Remover(id);
            return CustomResponse(galeriaItensViewModel);
        }
        #endregion

    }
}