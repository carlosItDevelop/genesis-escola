using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Controllers
{
    public class FilosofiaController : Controller
    {
        #region Variaveis
        private readonly FilosofiaApiClient _apiFilosofia;
        #endregion

        #region Construtor
        public FilosofiaController(FilosofiaApiClient apiFilosofia)
        {
            _apiFilosofia = apiFilosofia;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var modelFilosofia = await _apiFilosofia.BuscarAsync();
            if (modelFilosofia == null) modelFilosofia = new FilosofiaViewModel();
            return View(modelFilosofia);
        }
        #endregion
    }
}