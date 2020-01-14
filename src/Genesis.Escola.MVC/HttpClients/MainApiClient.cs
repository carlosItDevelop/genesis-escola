using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.HttpClients
{
    public abstract class MainApiClient
    {
        private readonly IHttpContextAccessor _httpAcessor;
        private readonly HttpClient _httpClient;
        private readonly HttpRequest _httpRequest;
        private readonly HttpResponse _httpResponse;
        public MainApiClient(HttpClient httpClient, IHttpContextAccessor httpAcessor)
        {
            _httpClient = httpClient;
            _httpAcessor = httpAcessor;
           // if (!_httpAcessor.HttpContext.User.Identity.IsAuthenticated) new RedirectResult("/Usuario/Login");// _httpResponse.Redirect("~/Login");
            //_httpRequest = httpRequest;
            //_httpResponse = httpResponse;
        }

        #region Pegar o Token
        public void AddToken()
        {

            //if (!string.IsNullOrEmpty(_httpRequest.Cookies[".Escola.Security.Cookie"]))
            //{
              //  _httpResponse.Redirect("~/Login");
            //}
                var token = _httpAcessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
              _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion
    }
}
