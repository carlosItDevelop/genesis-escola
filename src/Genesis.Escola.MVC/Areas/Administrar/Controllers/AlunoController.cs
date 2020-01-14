using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class AlunoController : Controller
    {
        #region Variaveis
        private readonly AlunosApiClient _api;
        private readonly TurmaAcadescApiClient _apiTurma;
        #endregion

        #region Construtor
        public AlunoController(AlunosApiClient api, TurmaAcadescApiClient apiTurma)
        {
            _api = api;
            _apiTurma = apiTurma;
        }
        #endregion

        #region Buscar
        [Area("Administrar")]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            return View(model);
        }
        #endregion

        #region Editar

        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id)
        {
            //ViewBag.Turma = new SelectList(
            //        await _apiTurma.BuscarAsync(),
            //        "Id",
            //        "Nome");


            var model = await _api.BuscarAsync(id);
            var turma = await _apiTurma.BuscarAsync(model.Serie, model.Turma, model.Turno);
            ViewBag.Turma = turma.Nome;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id, AlunosViewModel model)
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