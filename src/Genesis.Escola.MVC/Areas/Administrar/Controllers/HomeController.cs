using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Area("Administrar")]
    [Authorize]
    public class HomeController : MainController
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ClaimsPrincipal _claimsPrincipal;
        public HomeController(IHttpContextAccessor accessor) : base(accessor)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}