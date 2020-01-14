using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Genesis.Escola.MVC.Controllers
{
    public class MissaoController : Controller
    {
        #region Variaveis
        private readonly MissaoApiClient _apiMissao;
        #endregion

        #region Construtor
        public MissaoController(MissaoApiClient apiMissao)
        {
            _apiMissao = apiMissao;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var modelMissao = await _apiMissao.BuscarAsync();
            if (modelMissao == null) modelMissao = new MissaoViewModel();
            return View(modelMissao);
        }
        #endregion
    }
}