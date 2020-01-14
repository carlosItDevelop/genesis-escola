using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Genesis.Escola.MVC.Controllers
{
    public class GaleriaController : Controller
    {
        #region Variaveis
        private readonly GaleriaApiClient _apiGaleria;
        private readonly GaleriaItemApiClient _apiGaleriaItens;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public GaleriaController(GaleriaApiClient apiGaleria,
                                IConfiguration configuration,
                                GaleriaItemApiClient apiGaleriaItens)
        {
            _apiGaleria = apiGaleria;
            _apiGaleriaItens = apiGaleriaItens;
            _configuration = configuration;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var modelGaleria = await _apiGaleria.BuscarAsync();
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Galeria/PegarImagem/";
            return View(modelGaleria);
        }
        #endregion

        #region Item Galeria
        public async Task<ActionResult> Galerias(Guid Id)
        {
            var model = await _apiGaleria.BuscarAsync(Id);
            ViewData["Titulo"] = model.Titulo;
            ViewData["Descricao"] = model.Descricao;
            // return View(model);
            var modelG = await _apiGaleriaItens.BuscarTodosPorGaleria(Id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/GaleriaItem/PegarImagem/";
            return View(modelG);

            // var retorno = new List<GaleriaItens>();
            //var json = await ConsumirWebApi.RequestGET("v1/GaleriaItem/BuscarFotos", Id.ToString());
            // retorno = JsonConvert.DeserializeObject<List<GaleriaItens>>(json);
            // if (retorno.Count == 0) retorno = new List<GaleriaItens>();
            // ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/GaleriaItem/PegarImagem/";
            // return View(retorno);
        }
        #endregion
    }
}