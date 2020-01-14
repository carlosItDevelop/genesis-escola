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
    public class SobreController : MainController
    {
        #region Variaveis
        private readonly ISobreRepositorio _sobreRepositorio;
        private readonly IMapper _mapper;
        private readonly ISobreService _sobreService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public SobreController(ISobreRepositorio sobreRepositorio,
                               IMapper mapper,
                               ISobreService sobreService,
                               IHostingEnvironment env,
                               INotificador notificador,
                               IUser user) : base(notificador, user)
        {
            _sobreRepositorio = sobreRepositorio;
            _mapper = mapper;
            _sobreService = sobreService;
            _env = env;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<SobreViewModel> Obter()
        {
            return _mapper.Map<SobreViewModel>(await _sobreRepositorio.PegarDadosSobre());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<SobreViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<SobreViewModel>(await _sobreRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<SobreViewModel>> Adicionar(SobreViewModel sobreViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + sobreViewModel.CaminhoImagemPrincipal;
            var caminho = "/Paginas/Imagem/Sobre/";
            var caminhoAmbiente = _env.WebRootPath;

            var gravaImagem = Imagens.UploadArquivo(sobreViewModel.ImagemUpload, "SobrePrincipal", caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }

            sobreViewModel.CaminhoImagemPrincipal = gravaImagem.Value;
            var result = await _sobreService.Adicionar(_mapper.Map<Sobre>(sobreViewModel));
            return CustomResponse(sobreViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<SobreViewModel>> Atualizar(Guid id, SobreViewModel sobreViewModel)
        {
            if (id != sobreViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado");
                return CustomResponse(sobreViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (sobreViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Imagem/Sobre/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaImagem = Imagens.UploadArquivo(sobreViewModel.ImagemUpload, "SobrePrincipal", caminho,
                    caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return CustomResponse(gravaImagem.Value);
                }
                // excluir a imagem anterior 
                System.IO.File.Delete(_env.WebRootPath + sobreViewModel.CaminhoImagemPrincipal);
                //adicionar a nova imagem
                sobreViewModel.CaminhoImagemPrincipal = gravaImagem.Value;
            }
            await _sobreService.Atualizar(_mapper.Map<Sobre>(sobreViewModel));
            return CustomResponse(sobreViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SobreViewModel>> Excluir(Guid id)
        {
            var sobreViewModel = await _sobreRepositorio.ObterPorId(id);
            if (sobreViewModel == null) return NotFound();
            await _sobreService.Remover(id);
            return CustomResponse(sobreViewModel);
        }
        #endregion

        #region Pegar Imagem do Servidor
        [HttpGet]
        [Route("PegarImagem/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> ImageGetAsync(Guid Id)
        {
            var sobre = _mapper.Map<SobreViewModel>(await _sobreRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + sobre.CaminhoImagemPrincipal;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion
    }
}