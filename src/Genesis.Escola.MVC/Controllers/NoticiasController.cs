using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Controllers
{
    public class NoticiasController : Controller
    {
        #region Variaveis
        private readonly NoticiasApiClient _apiNoticia;
        #endregion

        #region Construtor
        public NoticiasController(NoticiasApiClient apiNoticia)
        {
            _apiNoticia = apiNoticia;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(Guid id)
        {
            var modelNoticia = await _apiNoticia.BuscarAsync(id);
            if (modelNoticia == null) modelNoticia = new NoticiasViewModel();
            return View(modelNoticia);
        }
        #endregion
    }
}