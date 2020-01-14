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
    [Authorize]
    public class PoloController : MainController
    {
        #region Variaveis
        private readonly IPoloRepositorio _poloRepositorio;
        private readonly IMapper _mapper;
        private readonly IPoloService _poloService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public PoloController(IPoloRepositorio poloRepositorio,
                               IMapper mapper,
                               IPoloService poloService,
                               IHostingEnvironment env,
                               INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _poloRepositorio = poloRepositorio;
            _mapper = mapper;
            _poloService = poloService;
            _env = env;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<PoloViewModel> ObterTodos()
        {
            return _mapper.Map<PoloViewModel>(await _poloRepositorio.PegarDados());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<PoloViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<PoloViewModel>(await _poloRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<PoloViewModel>> Adicionar(PoloViewModel poloViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var caminho = "/Paginas/Imagem/Polo/";
            var caminhoAmbiente = _env.WebRootPath;
            var gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload1, "curso1", caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }
            poloViewModel.CaminhoImagem1 = gravaImagem.Value;
            gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload2, "curso2", caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }
            poloViewModel.CaminhoImagem2 = gravaImagem.Value;
            gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload3, "curso3", caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }
            poloViewModel.CaminhoImagem3 = gravaImagem.Value;
            var result = await _poloService.Adicionar(_mapper.Map<Polo>(poloViewModel));
            return CustomResponse(poloViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PoloViewModel>> Atualizar(Guid id, PoloViewModel poloViewModel)
        {
            if (id != poloViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(poloViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var caminho = "/Paginas/Imagem/Polo/";
            var caminhoAmbiente = _env.WebRootPath;
            if (poloViewModel.ImagemUpload1 != null)
            {
                var gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload1, "curso1", caminho, caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return BadRequest(gravaImagem.Value);
                }
                poloViewModel.CaminhoImagem1 = gravaImagem.Value;
            }
            if (poloViewModel.ImagemUpload2 != null)
            {
                var gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload2, "curso2", caminho, caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return BadRequest(gravaImagem.Value);
                }
                poloViewModel.CaminhoImagem2 = gravaImagem.Value;
            }
            if (poloViewModel.ImagemUpload3 != null)
            {
                var gravaImagem = Imagens.UploadArquivo(poloViewModel.ImagemUpload3, "curso3", caminho, caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return BadRequest(gravaImagem.Value);
                }
                poloViewModel.CaminhoImagem3 = gravaImagem.Value;
            }

            await _poloService.Atualizar(_mapper.Map<Polo>(poloViewModel));
            return CustomResponse(poloViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PoloViewModel>> Excluir(Guid id)
        {
            var poloViewModel = await _poloRepositorio.ObterPorId(id);
            if (poloViewModel == null) return NotFound();
            await _poloService.Remover(id);
            return CustomResponse(poloViewModel);
        }
        #endregion

        #region Pegar Imagem do Servidor
        [HttpGet]
        [Route("PegarImagem1/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PegarImagem1(Guid Id)
        {
          //  var response = new HttpResponseMessage(HttpStatusCode.OK);
            var polo = _mapper.Map<PoloViewModel>(await _poloRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + polo.CaminhoImagem1;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }

        [HttpGet]
        [Route("PegarImagem2/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PegarImagem2(Guid Id)
        {
           // var response = new HttpResponseMessage(HttpStatusCode.OK);
            var polo = _mapper.Map<PoloViewModel>(await _poloRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + polo.CaminhoImagem2;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }

        [HttpGet]
        [Route("PegarImagem3/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> PegarImagem3(Guid Id)
        {
           // var response = new HttpResponseMessage(HttpStatusCode.OK);
            var polo = _mapper.Map<PoloViewModel>(await _poloRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + polo.CaminhoImagem3;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion

    }
}