using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;


namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class AdminErrosController : Controller
    {
        private ILogger<AdminErrosController> _logger;
        public AdminErrosController(ILogger<AdminErrosController> logger)
        {
            _logger = logger;
        }

        [Area("Administrar")]
        public IActionResult Erro500()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogDebug(exception.Error, "Ocorreu um erro não tratado pelo sistema");
            string contem = "401 (Unauthorized)";
            if (exception.Error.Message.Contains(contem)) ViewBag.CodigoErro = "401";
            else ViewBag.CodigoErro = "500";
            ViewBag.Mensagem = exception.Error.Message;
            return View();
        }
    }
}