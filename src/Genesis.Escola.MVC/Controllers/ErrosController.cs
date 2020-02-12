using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Genesis.Escola.MVC.Controllers
{
    public class ErrosController : Controller
    {
        private ILogger<ErrosController> _logger;
        public ErrosController(ILogger<ErrosController> logger)
        {
            _logger = logger;
        }

        public IActionResult Erro500()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogDebug(exception.Error, "Ocorreu um erro não tratado pelo sistema");
            string contem = "401 (Unauthorized)";

            ViewBag.Mensagem = exception.Error.Message;
            return View();
        }

        //[Route("Erros/{statusCode}")]
        //public IActionResult ErroGenerico(int statusCode)
        //{
        //    ViewBag.StatusCode = statusCode;
        //    ViewBag.Mensagem = ReasonPhrases.GetReasonPhrase(statusCode);
        //    return View();
        //}

        [Route("Erros/{statusCode}")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404 || statusCode == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View();
        }

        [Route("Erros/401")]
        public IActionResult Erro401()
        {

            return View();
        }

        [Route("Erros/404")]
        public IActionResult Erro404()
        {

            return View();
        }

        [Route("Erros/403")]
        public IActionResult Erro403()
        {

            return View();
        }

        [Route("Erros/503")]
        public IActionResult Erro503()
        {

            return View();
        }
    }
}