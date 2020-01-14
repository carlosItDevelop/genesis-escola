using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Area("Administrar")]
    public class SobreController : Controller
    {
        #region Variaveis
        private readonly SobreApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public SobreController(IHttpContextAccessor acessor,
                               SobreApiClient api,
                               IConfiguration configuration)
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
                ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Sobre/PegarImagem/" + model.Id;
                return View("Alterar", model);
            }
        }
        #endregion

        #region Incluir
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Incluir(SobreViewModel model, IFormFile file)
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

        #region Alterar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Alterar(SobreViewModel model, IFormFile file)
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
    }
}