using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class PoloController : Controller
    {
        #region Variaveis
        private readonly PoloApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public PoloController(PoloApiClient api, IConfiguration configuration)
        {
            _api = api;
            _configuration = configuration;
        }
        #endregion

        #region Buscar Sobre
        [Area("Administrar")]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            if (model == null) return View("Incluir", model);
            else
            {
                ViewData["CaminhoImagem1"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem1/" + model.Id;
                ViewData["CaminhoImagem2"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem2/" + model.Id;
                ViewData["CaminhoImagem3"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem3/" + model.Id;
                return View("Alterar", model);
            }
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Incluir(PoloViewModel model, IEnumerable<IFormFile> File1)
        {
            var imagemConta = 1;
            foreach (var file in File1)
            {
                if (file == null && file.Length == 0)
                {
                    ModelState.AddModelError("", $" {"Imagem "} {imagemConta} {" é obrigatória"}");
                }
                else if (file.Length > 2009393)
                {
                    ModelState.AddModelError("", $" {"Imagem do Curso "} {imagemConta} {" maior que 2 Mb"}");
                }
                using (MemoryStream mStream = new MemoryStream())
                {
                    await file.CopyToAsync(mStream);
                    byte[] bytes = mStream.ToArray();
                    if (imagemConta == 1) model.ImagemUpload1 = bytes;
                    if (imagemConta == 2) model.ImagemUpload2 = bytes;
                    if (imagemConta == 3) model.ImagemUpload3 = bytes;
                }
                imagemConta++;
            }

            if (ModelState.IsValid)
            {
                await _api.IncluirAsync(model);
                return RedirectToAction(nameof(Alterar));
            }
            return View(model);
        }
        #endregion

        #region Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Alterar(PoloViewModel model, IEnumerable<IFormFile> File1)
        {
            var imagemConta = 1;
            foreach (var file in File1)
            {
                if (file != null && file.Length > 0)
                {
                    if (file.Length > 2009393)
                    {
                        ModelState.AddModelError("", $" {"Imagem do Curso "} {imagemConta} {" maior que 2 Mb"}");
                    }
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        await file.CopyToAsync(mStream);
                        byte[] bytes = mStream.ToArray();
                        if (file.FileName == "course1.jpg") model.ImagemUpload1 = bytes;
                        if (file.FileName == "course2.jpg") model.ImagemUpload2 = bytes;
                        if (file.FileName == "course3.jpg") model.ImagemUpload3 = bytes;
                    }
                }
                imagemConta++;
            }

            if (ModelState.IsValid)
            {
                await _api.AlterarAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

    }
}