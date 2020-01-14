using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Controllers
{
    public class SobreController : Controller
    {
        #region Variaveis
        private readonly SobreApiClient _apiSobre;
        #endregion

        #region Construtor
        public SobreController(SobreApiClient apiSobre)
        {
            _apiSobre = apiSobre;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var modelSobre = await _apiSobre.BuscarAsync();
            if (modelSobre == null) modelSobre = new SobreViewModel();
            return View(modelSobre);
        }
        #endregion
    }
}