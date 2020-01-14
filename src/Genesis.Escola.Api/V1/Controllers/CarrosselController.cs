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
using static Genesis.Escola.Business.Enumeradores.Enumeradores;

namespace Genesis.Escola.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CarrosselController : MainController
    {
        #region Variaveis
        private readonly ICarrosselRepositorio _carrosselRepositorio;
        private readonly IMapper _mapper;
        private readonly ICarrosselService _carrosselService;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Construtor
        public CarrosselController(ICarrosselRepositorio carrosselRepositorio,
                               IMapper mapper,
                               ICarrosselService carrosselService,
                               INotificador notificador,
                               IHostingEnvironment env,
                               IUser user) : base(notificador, user)
        {
            _env = env;
            _carrosselRepositorio = carrosselRepositorio;
            _mapper = mapper;
            _carrosselService = carrosselService;
        }
        #endregion

        #region Get

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CarrosselViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CarrosselViewModel>>(await _carrosselRepositorio.ObterTodos());
        }


        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarrosselViewModel>> ObterPorId(Guid id)
        {
            var entidade = _mapper.Map<CarrosselViewModel>(await _carrosselRepositorio.ObterPorId(id));
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ObterAtivoInativo/{tipo}")]
        public async Task<IEnumerable<CarrosselViewModel>> ObterAtivoInativo(EnumAtivoInativo tipo)
        {
            return _mapper.Map<IEnumerable<CarrosselViewModel>>(await _carrosselRepositorio.Buscar(c => c.Ativo == tipo.ToString().Substring(0, 1)));
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<CarrosselViewModel>> Adicionar(CarrosselViewModel carrosselViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + carrosselViewModel.CaminhoImagem;
            var caminho = "/Paginas/Imagem/Carrossel/";
            var caminhoAmbiente = _env.WebRootPath;

            var gravaImagem = Imagens.UploadArquivo(carrosselViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho, caminhoAmbiente, false);
            if (gravaImagem.Key == 1)
            {
                return CustomResponse(gravaImagem.Value);
            }

            carrosselViewModel.CaminhoImagem = gravaImagem.Value;
            await _carrosselService.Adicionar(_mapper.Map<Carrossel>(carrosselViewModel));
            return CustomResponse(carrosselViewModel);
        }
        #endregion

        #region Put
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarrosselViewModel>> Atualizar(Guid id, CarrosselViewModel carrosselViewModel)
        {
            if (id != carrosselViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(carrosselViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);
            if (carrosselViewModel.ImagemUpload != null)
            {
                var caminho = "/Paginas/Imagem/Carrossel/";
                var caminhoAmbiente = _env.WebRootPath;
                var gravaImagem = Imagens.UploadArquivo(carrosselViewModel.ImagemUpload, Guid.NewGuid().ToString(), caminho,
                    caminhoAmbiente, false);
                if (gravaImagem.Key == 1)
                {
                    return CustomResponse(gravaImagem.Value);
                }
                // excluir a imagem anterior 
                System.IO.File.Delete(_env.WebRootPath + carrosselViewModel.CaminhoImagem);
                //adicionar a nova imagem
                carrosselViewModel.CaminhoImagem = gravaImagem.Value;
            }

            await _carrosselService.Atualizar(_mapper.Map<Carrossel>(carrosselViewModel));
            return CustomResponse(carrosselViewModel);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CarrosselViewModel>> Excluir(Guid id)
        {
            var carrosselViewModel = await _carrosselRepositorio.ObterPorId(id);
            if (carrosselViewModel == null) return NotFound();
            await _carrosselService.Remover(id);
            System.IO.File.Delete(_env.WebRootPath + carrosselViewModel.CaminhoImagem);
            return CustomResponse(carrosselViewModel);
        }
        #endregion

        #region Pegar Imagem do Servidor
        [HttpGet]
        [Route("PegarImagem/{id:Guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> ImageGetAsync(Guid Id)
        {
            var galeria = _mapper.Map<CarrosselViewModel>(await _carrosselRepositorio.ObterPorId(Id));
            var webRoot = _env.WebRootPath + galeria.CaminhoImagem;
            var ext = Path.GetExtension(webRoot);
            var contents = System.IO.File.ReadAllBytes(webRoot);
            return File(contents, "image/" + ext);
        }
        #endregion
    }
}
