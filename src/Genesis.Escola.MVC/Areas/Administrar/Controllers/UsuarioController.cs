using Genesis.Escola.MVC.Areas.Administrar.Models;
using Genesis.Escola.MVC.Functions;
using Genesis.Escola.MVC.HttpClients;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    [Authorize]
    [Area("Administrar")]
    public class UsuarioController : Controller
    {
        #region Variaveis
        private readonly UsuarioApiClient _api;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public UsuarioController(UsuarioApiClient api, IConfiguration configuration)
        {
            _api = api;
            _configuration = configuration;
        }
        #endregion


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            return View(model);
        }


        #region Editar

        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id)
        {
            var model = await _api.BuscarAsync(id.ToString());
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Area("Administrar")]
        //public async Task<ActionResult> Editar(Guid id, UsuarioViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _api.AlterarAsync(model.Id, model);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}
        #endregion


        #region Login
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        //  [Route("Administrar")]
        public async Task<ActionResult> Login(LoginUserViewModel dados)
        {
            var retorno1 = await _api.obterLogin(dados);
            var retorno = await ConsumirWebApi.RequestPOST<LoginUserViewModel>("v1/Auth/login", dados);
            if (!retorno1.Success) return View(dados);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, retorno1.Data.UserToken.NomeCompleto));
            claims.Add(new Claim("Token", retorno1.Data.AccessToken));
            foreach (var item in retorno1.Data.UserToken.Claims)
            {
                claims.Add(new Claim(item.Type, item.Value));
            }


            //e guardar esses direitos na identidade principal AspNetCore.Cookies
            var claimsIdentity = new ClaimsIdentity(
                claims,
                "cookie"
            );

            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            var authProp = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow,
                //configurar expiração do cookie para um valor menor que a expiração do token
                //retiro 120 segundos do expiração para o cookie expirar antes 
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(retorno1.Data.ExpiresIn - 120),
                IsPersistent = true
            };

            //faz autenticação via Cookie
            var identity = new ClaimsIdentity(claims, "EscolaSecurityScheme");
            await HttpContext.SignInAsync(scheme: "EscolaSecurityScheme",
                principal: principal, authProp);

            if (identity.IsAuthenticated) return RedirectToAction("Index", "Home");
            return RedirectToAction(nameof(Login));

        }
        #endregion

        #region Logout
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
        scheme: "EscolaSecurityScheme");
            //  await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }

    public class ClaimJson
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class UserToken
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string nomeCompleto { get; set; }
        public List<ClaimJson> claims { get; set; }
    }

    public class Data
    {
        public string accessToken { get; set; }
        public double expiresIn { get; set; }
        public UserToken userToken { get; set; }
    }

    public class RetornoJson
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }
}
