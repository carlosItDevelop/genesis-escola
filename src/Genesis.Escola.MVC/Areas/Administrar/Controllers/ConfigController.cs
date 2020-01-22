using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public class ConfigController : Controller
    {
        #region Variaveis
        private readonly ConfigApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public ConfigController(ConfigApiClient api, IConfiguration configuration)
        {
            _api = api;
            _configuration = configuration;
        }
        #endregion

        #region Buscar 
        [Area("Administrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Basico()
        {
            var model = await _api.BuscarAsync();
            if (model == null)
            {
                model = new ConfigViewModel();
                return View("IncluirBasico", model);
            }
            else
            {
                ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Config/PegarImagem/" + model.Id;
                return View("AlterarBasico", model);
            }
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> IncluirBasico(ConfigViewModel model, IFormFile file)
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
                var resultado = await _api.IncluirAsync(model);
                if (resultado.Success) return RedirectToAction("Basico");
                else
                {
                    foreach (var item in resultado.errors)
                    {
                        ModelState.AddModelError("", item.ToString());
                    }

                }
            }
            return View(model);
        }
        #endregion

        #region Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> AlterarBasico(ConfigViewModel model, IFormFile file)
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
            if (string.IsNullOrEmpty(model.ImagemYoutube))
            {
                if (file == null)
                {
                    ModelState.AddModelError("", "A Imagem do Youtube é obrigatória");
                }
            }

            if (ModelState.IsValid)
            {
                var resultado = await _api.AlterarAsync(model.Id, model);
                if (resultado.Success) return RedirectToAction("Basico");
                else
                {
                    foreach (var item in resultado.errors)
                    {
                        ModelState.AddModelError("", item.ToString());
                    }

                }
            }
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Config/PegarImagem/" + model.Id;
            return View(model);
        }
        #endregion
    }
}