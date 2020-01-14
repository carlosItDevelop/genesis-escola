using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public abstract class MainController : Controller
    {
        private readonly IHttpContextAccessor _acesso;
        private readonly ClaimsPrincipal _claimsPrincipal;


        protected MainController(IHttpContextAccessor accessor)
        {
            _acesso = accessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!User.Identity.IsAuthenticated)
            {
                RedirectToAction("Login", "Usuario");
            }
            // Do something before the action executes.
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            base.OnActionExecuted(context);
        }

    }
}