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
    public class ConfigController : MainController
    {
        #region Variaveis
        private readonly IConfigRepositorio _configRepositorio;
        private readonly IMapper _mapper;
        private readonly IConfigService _configService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public ConfigController(IConfigRepositorio configRepositorio,
                               IMapper mapper,
                               IConfigService configService, 
                               INotificador notificador,
                               IHostingEnvironment env,
                               IUser user) : base(notificador, user)
        {
            _configRepositorio = configRepositorio;
            _mapper = mapper;
            _configService = configService;
            _env = env;
        }
        #endregion

        #region Get
        [HttpGet]
        [AllowAnonymous]
        public async Task<ConfigViewModel> Obter()
        {
            return _mapper.Map<ConfigViewModel>(await _configRepositorio.PegarDados());
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<ConfigViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<ConfigViewModel>(await _configRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<ConfigViewModel>> Adicionar(ConfigViewModel configViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var caminho = @"\imagens\";
            var caminhoAmbiente = _env.WebRootPath;

            var gravaImagem = Imagens.UploadArquivo(configViewModel.ImagemUpload, "video", caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }

            configViewModel.ImagemYoutube = gravaImagem.Value;

            var result = await _configService.Adicionar(_mapper.Map<Config>(configViewModel));
            return CustomResponse(configViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ConfigViewModel>> Atualizar(Guid id, ConfigViewModel configViewModel)
        {
            if (id != configViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(configViewModel);
            }

            if (configViewModel.ImagemUpload != null)
            {
                // excluir a imagem anterior 
                if (!string.IsNullOrEmpty(configViewModel.ImagemYoutube)) System.IO.File.Delete(_env.WebRootPath + configViewModel.ImagemYoutube);

                var caminho = @"\imagens\";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaImagem = Imagens.UploadArquivo(configViewModel.ImagemUpload, "video", caminho,
                    caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return CustomResponse(gravaImagem.Value);
                }
                //adicionar a nova imagem
                configViewModel.ImagemYoutube = gravaImagem.Value;
            }


            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _configService.Atualizar(_mapper.Map<Config>(configViewModel));
            return CustomResponse(configViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ConfigViewModel>> Excluir(Guid id)
        {
            var configViewModel = await _configRepositorio.ObterPorId(id);
            if (configViewModel == null) return NotFound();
            await _configService.Remover(id);
            return CustomResponse(configViewModel);
        }
        #endregion

        #region Pegar Imagem do Servidor
        [HttpGet]
        [Route("PegarImagem/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> ImageGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<ConfigViewModel>(await _configRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.ImagemYoutube;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion

    }
}