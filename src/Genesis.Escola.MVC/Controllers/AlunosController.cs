using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Genesis.Escola.MVC.Controllers
{
    public class AlunosController : Controller
    {

        #region Variaveis
        private readonly AlunosApiClient _apiAlunos;
        private readonly TurmaAcadescApiClient _apiTurma;
        private readonly ComunicadoApiClient _apiComunicado;
        private readonly CronogramaApiClient _apiCronograma;
        private readonly TarefaApiClient _apiTarefa;
        private readonly NotasApiClient _apiNotas;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public AlunosController(AlunosApiClient apiAlunos,
                                IHttpContextAccessor httpContextAccessor,
                                ComunicadoApiClient apiComunicado,
                                CronogramaApiClient apiCronograma,
                                TarefaApiClient apiTarefa,
                                IConfiguration configuration,
                                NotasApiClient apiNotas,
                                TurmaAcadescApiClient apiTurma)
        {
            _apiAlunos = apiAlunos;
            _httpContextAccessor = httpContextAccessor;
            _apiComunicado = apiComunicado;
            _configuration = configuration;
            _apiCronograma = apiCronograma;
            _apiTurma = apiTurma;
            _apiNotas = apiNotas;
            _apiTarefa = apiTarefa;
        }
        #endregion

        #region Login do Aluno
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AlunosViewModel dados)
        {
            var retorno1 = await _apiAlunos.LogarAsync(dados.Matricula, dados.Senha);

            if (retorno1 == null)
            {
                Remove("AlunoLogado");
                return View(dados);
            }
            Set("AlunoLogado", retorno1.Matricula, 2600);
            Set("AlunoNome", retorno1.Nome, 2600);
            Set("AlunoSenha", retorno1.Senha, 2600);
            Set("AlunoTurma", retorno1.Serie + "-" + retorno1.Turma + "-" + retorno1.Turno+ "-" + retorno1.Curso, 2600);
            return RedirectToAction(nameof(Perfil));
        }
        #endregion

        #region Perfil do Aluno
        public async Task<ActionResult> Perfil()
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var retorno1 = await _apiAlunos.BuscarPorMatricula(PegarAlunoLogado());

            var turma = await _apiTurma.BuscarAsync(retorno1.Serie, retorno1.Turma, retorno1.Turno);
            retorno1.Turma = turma.Nome;
            return View(retorno1);
        }
        #endregion

        #region Comunicados
        public async Task<ActionResult> Comunicados()
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var modelComunicado = await _apiComunicado.BuscarAtivas(PegarAlunoTurma());
            return View(modelComunicado);
        }

        public async Task<ActionResult> ExibirComunicado(Guid Id)
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            
            var modelComunicado = await _apiComunicado.BuscarAsync(Id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Comunicado/PegarPdf/";
            return View(modelComunicado);
        }

        #endregion

        #region Cronograma
        public async Task<ActionResult> Cronograma()
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var modelCronograma = await _apiCronograma.BuscarAtivas(PegarAlunoTurma());
            return View(modelCronograma);
        }

        public async Task<ActionResult> ExibirCronograma(Guid Id)
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var modelCronograma = await _apiCronograma.BuscarAsync(Id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Cronograma/PegarPdf/";
            return View(modelCronograma);
        }

        #endregion

        #region Tarefas
        public async Task<ActionResult> Tarefas()
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var modelCronograma = await _apiTarefa.BuscarAtivas(PegarAlunoTurma());
            return View(modelCronograma);
        }

        public async Task<ActionResult> ExibirTarefa(Guid Id)
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            var modelCronograma = await _apiTarefa.BuscarAsync(Id);
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Tarefa/PegarPdf/";
            return View(modelCronograma);
        }

        #endregion

        #region Boletim
        public async Task<ActionResult> Boletim()
        {
            if (!AlunoLogado()) return RedirectToAction(nameof(Login));
            List<NotasViewModel> modelNotas = await _apiNotas.BuscarBoletim(PegarAlunoLogado(),PegarAlunoSenha());
            var newList = modelNotas.OrderBy(x => x.Materia).ToList();
            return View(newList);
        }

        #endregion

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }

        public bool AlunoLogado()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["AlunoLogado"];
            return (!string.IsNullOrEmpty(cookieValueFromContext));
        }

        public string PegarAlunoLogado()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["AlunoLogado"];
            return (cookieValueFromContext);
        }

        public string PegarAlunoSenha()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["AlunoSenha"];
            return (cookieValueFromContext);
        }
        public string PegarAlunoTurma()
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["AlunoTurma"];
            return (cookieValueFromContext);
        }

    }
}