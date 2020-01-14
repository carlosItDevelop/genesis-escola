using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class TurmaController : Controller
    {
        #region Variaveis
        private readonly TurmaAcadescApiClient _api;
        #endregion

        #region Construtor
        public TurmaController(TurmaAcadescApiClient api)
        {
            _api = api;
        }
        #endregion

        #region Buscar
        [Area("Administrar")]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            return View(model.OrderBy(t => t.Nome));
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
        public async Task<ActionResult> Adicionar(TurmaAcadescViewModel model)
        {
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
        public async Task<ActionResult> Editar(Guid id)
        {
            var model = await _api.BuscarAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id, TurmaAcadescViewModel model)
        {
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
            var model = await _api.BuscarAsync(id);
            return View(model);
        }


        [Area("Administrar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id, TurmaAcadescViewModel dados)
        {
            var model = await _api.BuscarAsync(id);
            if (model != null)
            {
                await _api.ExcluirAsync(id);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Registro não encontrado para Exluir!!! (Pode ter sido excluido por outra pessoa)");
            return View();
        }
        #endregion
    }
}