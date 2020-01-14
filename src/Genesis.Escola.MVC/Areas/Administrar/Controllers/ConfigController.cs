using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Authorize]
    [Area("Administrar")]
    public class ConfigController : Controller
    {
        #region Variaveis
        private readonly ConfigApiClient _api;
        #endregion

        #region Construtor
        public ConfigController(ConfigApiClient api)
        {
            _api = api;
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
            else return View("AlterarBasico", model);
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> IncluirBasico(ConfigViewModel model)
        {
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
        public async Task<ActionResult> AlterarBasico(ConfigViewModel model)
        {
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
            return View(model);
        }
        #endregion
    }
}