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
    public class GaleriaItemController : Controller
    {
        #region Variaveis
        private readonly GaleriaItemApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public GaleriaItemController(GaleriaItemApiClient api, 
                                     IConfiguration configuration)
        {
            _api = api;
            _configuration = configuration;
        }
        #endregion

        #region Adicionar
        [Area("Administrar")]
        public IActionResult Adicionar(Guid Id)
        {
            GaleriaItensViewModel model = new GaleriaItensViewModel();
            model.GaleriaId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Adicionar(GaleriaItensViewModel model, IFormFile file)
        {
            if (file == null && file.Length == 0)
            {
                ModelState.AddModelError("", "A Imagem é obrigatória");
            }
            else if (file.Length > 2009393)
            {
                ModelState.AddModelError("", "A Imagem é maior que 2 Mb");
            }
            if (ModelState.IsValid)
            {
                model.Id = Guid.NewGuid();
                using (MemoryStream mStream = new MemoryStream())
                {
                    await file.CopyToAsync(mStream);
                    byte[] bytes = mStream.ToArray();
                    model.ImagemUpload = bytes;
                }
                await _api.IncluirAsync(model);



                return RedirectToAction("Fotos", "Galeria", new { Id = model.GaleriaId });
            }
            return View(model);
        }
        #endregion

        #region Fotos Galeria Item

        [Area("Administrar")]
        public async Task<ActionResult> Fotos(Guid id)
        {
            var model = await _api.BuscarAsync(id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/GaleriaItem/PegarImagem/" + id;
            return View(model);

        }

        //ToDo: Buscar as imagens

        #endregion


    }
}