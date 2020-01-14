using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.Controllers
{
    public class CursosController : Controller
    {
        #region Variaveis
        private readonly CursosApiClient _apiCursos;
        #endregion

        #region Construtor
        public CursosController(CursosApiClient apiCursos)
        {
            _apiCursos = apiCursos;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(EnumCurso curso)
        {
            var modelCursos = await _apiCursos.BuscarAsync(curso);
            if (modelCursos == null) modelCursos = new CursoViewModel();
            return View(modelCursos);
        }
        #endregion
    }
}