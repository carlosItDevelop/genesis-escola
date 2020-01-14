using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class FilosofiaController : Controller
    {
        #region Variaveis
        private readonly FilosofiaApiClient _api;
        #endregion

        #region Construtor
        public FilosofiaController(FilosofiaApiClient api)
        {
            _api = api;
        }
        #endregion

        #region Buscar 
        [Area("Administrar")]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            if (model == null) return View("Incluir", model);
            else return View("Alterar", model);
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Incluir(FilosofiaViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _api.IncluirAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Alterar(FilosofiaViewModel model)
        {
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