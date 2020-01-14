using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Authorize]
    [Area("Administrar")]
    public class GaleriaController : Controller
    {
        #region Variaveis
        private readonly GaleriaApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public GaleriaController(GaleriaApiClient api, IConfiguration configuration)
        {
            _api = api;
            _configuration = configuration;
        }
        #endregion

        #region Buscar
        [Area("Administrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            return View(model);
        }
        #endregion

        #region Adicionar
        [Area("Administrar")]
        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Adicionar(GaleriaViewModel model, IFormFile file)
        {
            if (file == null && file.Length == 0)
            {
                ModelState.AddModelError("", "A Imagem é obrigatória");
            }
            else if (file.Length > 2009393)
            {
                ModelState.AddModelError("", "A Imagem é maior que 2 Mb");
            }
            using (MemoryStream mStream = new MemoryStream())
            {
                await file.CopyToAsync(mStream);
                byte[] bytes = mStream.ToArray();
                model.ImagemUpload = bytes;
            }

            if (ModelState.IsValid)
            {
                await _api.IncluirAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Editar

        [Area("Administrar")]
        public async Task<ActionResult> Alterar(Guid id)
        {
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Galeria/PegarImagem/" + id;
            var model = await _api.BuscarAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Alterar(GaleriaViewModel model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (file.Length > 2009393)
                {
                    ModelState.AddModelError("", "A Imagem é maior que 2 Mb");
                }
                using (MemoryStream mStream = new MemoryStream())
                {
                    await file.CopyToAsync(mStream);
                    byte[] bytes = mStream.ToArray();
                    model.ImagemUpload = bytes;
                }
            }


            if (ModelState.IsValid)
            {
                await _api.AlterarAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Excluir
        [Area("Administrar")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Galeria/PegarImagem/" + id;
            var model = await _api.BuscarAsync(id);
            return View(model);
        }


        [Area("Administrar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id, NoticiasViewModel dados)
        {
            var model = await _api.BuscarAsync(id);
            if (model != null)
            {
                await _api.ExcluirAsync(id);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Registro não encontrado para Exluir!!! (Pode ter sido excluido por outra pessoa)");
            return View();

            //ToDo: Fazer o excluir da Galeria

        }


        #endregion
        
        #region Fotos Galeria

        [Area("Administrar")]
        public async Task<ActionResult> Fotos(Guid id)
        {
            var model = await _api.BuscarAsync(id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Galeria/PegarImagem/" + id;
            return View(model);
        }

        #endregion
    }
}