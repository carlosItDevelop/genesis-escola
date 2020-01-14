using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Genesis.Escola.MVC.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Genesis.Escola.MVC.Utility;
using System.Dynamic;
using Genesis.Escola.MVC.HttpClients;

namespace Genesis.Escola.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Variaveis
        private readonly IOptions<SettingsModel> appSettings;
        private IConfiguration _configuration;
        private readonly NoticiasApiClient _api;
        private readonly SobreApiClient _apiSobre;
        private readonly CarrosselApiClient _apiCarrossel;
        private readonly PoloApiClient _apiPolo;
        #endregion

        public HomeController(IOptions<SettingsModel> app, 
                              IConfiguration configuration, 
                              NoticiasApiClient api,
                              SobreApiClient apiSobre,
                              CarrosselApiClient apiCarrossel,
                              PoloApiClient apiPolo)
        {
            appSettings = app;
            _configuration = configuration;
            _api = api;
            _apiSobre = apiSobre;
            _apiCarrossel = apiCarrossel;
            _apiPolo = apiPolo;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Escola Maximu´s";
            var model = await _api.BuscarAtivas();
            dynamic modelo = new ExpandoObject();
            modelo.Noticias = model.OrderByDescending(c => c.DataInicio);

            var modelSobre = await _apiSobre.BuscarAsync();
            ViewData["CaminhoImagemSobre"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Sobre/PegarImagem/"+modelSobre.Id;
            if (modelSobre == null) modelSobre = new SobreViewModel();
            modelo.Sobre = modelSobre;

            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Carrossel/PegarImagem/";
            var dfinalCarrossel = DateTime.Now.AddDays(60);
            var modelCarrossel = await _apiCarrossel.BuscarAtivoInativo(Functions.Enumeradores.EnumAtivoInativo.Ativo);
            modelo.Carrossel = modelCarrossel;

            var modelPolo = await _apiPolo.BuscarAsync();
            ViewData["CaminhoImagemPolo1"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem1/" + modelPolo.Id;
            ViewData["CaminhoImagemPolo2"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem2/" + modelPolo.Id;
            ViewData["CaminhoImagemPolo3"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Polo/PegarImagem3/" + modelPolo.Id;
            modelo.Polo = modelPolo;

            return View(modelo);
        }
    }
}
