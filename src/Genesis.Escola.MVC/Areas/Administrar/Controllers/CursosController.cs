using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Authorize]
    [Area("Administrar")]
    public class CursosController : Controller
    {
        #region Variaveis
        private readonly CursosApiClient _api;
        #endregion

        #region Construtor
        public CursosController(CursosApiClient api)
        {
            _api = api;
        }
        #endregion

        #region Buscar 
        [Area("Administrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(EnumCurso curso)
        {
            var model = await _api.BuscarAsync(curso);
            if (model == null)
            {
                model = new CursoViewModel();
                model.Curso = curso;
                return View("Incluir", model);
            }
            else return View("Alterar", model);
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Incluir(CursoViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _api.IncluirAsync(model);
                return RedirectToAction("Index", new { curso = model.Curso });
            }
            return View(model);
        }
        #endregion

        #region Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Alterar(CursoViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _api.AlterarAsync(model.Id, model);
                return RedirectToAction("Index", new { curso = model.Curso });
            }
            return View(model);
        }
        #endregion
    }
}