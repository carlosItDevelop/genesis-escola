using Genesis.Escola.MVC.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Areas.Administrar.ViewComponents
{
    public class GaleriaItemViewComponent : ViewComponent
    {
        #region Variaveis
       // private readonly IOptions<SettingsModel> appSettings;
        private IConfiguration _configuration;
        private readonly GaleriaItemApiClient _api;
        #endregion

        #region Construtor
        public GaleriaItemViewComponent(IConfiguration configuration, GaleriaItemApiClient api)
        {
            _configuration = configuration;
            _api = api;
        }
        #endregion

        #region InvokeAsync
        [Area("Administrar")]
        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
        {
            var model = await _api.BuscarTodosPorGaleria(Id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/GaleriaItem/PegarImagem/";
            return View(model);
        }
        #endregion
    }
}
