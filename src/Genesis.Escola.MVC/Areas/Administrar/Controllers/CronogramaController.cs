using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class CronogramaController : Controller
    {
        #region Variaveis
        private readonly CronogramaApiClient _api;
        private readonly CursoAcadescApiClient _apicursoA;
        private readonly TurmaAcadescApiClient _apiTurma;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public CronogramaController(CronogramaApiClient api,
                                    TurmaAcadescApiClient apiTurma,
                                    CursoAcadescApiClient apicursoA,
                                    IConfiguration configuration)
        {
            _api = api;
            _apiTurma = apiTurma;
            _configuration = configuration;
            _apicursoA = apicursoA;
        }
        #endregion

        #region Buscar
        [Area("Administrar")]
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            return View(model);
        }
        #endregion

        #region Adicionar
        [Area("Administrar")]
        public async Task<IActionResult> Adicionar()
        {
            //ViewBag.Turma = await BuscarTurma();
            ViewBag.Json = JsonConvert.SerializeObject(await BuscarTurmaGeral());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Adicionar(CronogramaViewModel model, IFormFile file, string selectedItems)
        {
            List<TreeViewNode> itemsRetornados = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            var turma = "";
            // ViewBag.Turma = await BuscarTurma();

            foreach (var item in itemsRetornados)
            {
                if (item.id.Length > 3) turma += item.id + '|';
            }


            if (file != null && file.Length > 0)
            {
                if (file != null && file.Length > 0)
                {
                    if (file.Length > 2009393)
                    {
                        ModelState.AddModelError("", "O Arquivo é maior que 2 Mb");
                    }
                }
            }
            if (string.IsNullOrEmpty(model.DescricaoCompleta) && file == null)
            {
                ModelState.AddModelError("", "Você deve ter uma Descrição Completa e/ou um arquivo PDF");
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        await file.CopyToAsync(mStream);
                        byte[] bytes = mStream.ToArray();
                        model.ImagemUpload = bytes;
                    }
                }
                model.TurmaId = turma;
                await _api.IncluirAsync(model);


                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Editar

        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id)
        {
            ViewBag.Turma = await BuscarTurma();
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Cronograma/PegarPdf/" + id;
            var model = await _api.BuscarAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id, CronogramaViewModel model, IFormFile file)
        {
            ViewBag.Turma = BuscarTurma();
            if (file != null && file.Length > 0)
            {
                if (file.Length > 2009393)
                {
                    ModelState.AddModelError("", "O Arquivo é maior que 2 Mb");
                }
            }
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        await file.CopyToAsync(mStream);
                        byte[] bytes = mStream.ToArray();
                        model.ImagemUpload = bytes;
                    }
                }
                await _api.AlterarAsync(model.Id, model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Excluir
        [Area("Administrar")]
        public async Task<ActionResult> Excluir(Guid id)
        {
            ViewBag.Turma = await BuscarTurma();
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Cronograma/PegarPdf/" + id;
            var model = await _api.BuscarAsync(id);
            return View(model);
        }


        [Area("Administrar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id, CronogramaViewModel dados)
        {
            ViewBag.Turma = await BuscarTurma();
            var model = await _api.BuscarAsync(id);
            if (model != null)
            {
                await _api.ExcluirAsync(id);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Registro não encontrado para Exluir!!! (Pode ter sido excluido por outra pessoa)");
            return View();
        }
        #endregion

        public async Task<List<SelectListItem>> BuscarTurma()
        {
            var turmaLista = new List<SelectListItem>();

            var turma = await _apiTurma.BuscarAsync();

            turmaLista.Add(new SelectListItem()
            {
                Text = "Todos",
                Value = "TTT"
            });
            foreach (var item in turma)
            {
                turmaLista.Add(new SelectListItem()
                {
                    Text = item.Nome,
                    Value = item.Serie.Trim() + "-" + item.Turma.Trim() + "-" + item.Turno.Trim()
                });
            }
            //  turmaLista.OrderBy(x => x.Text);
            return turmaLista.OrderBy(x => x.Text).ToList();
        }


        public async Task<List<TreeViewNode>> BuscarTurmaGeral(string turma = null)
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            var modelCurso = await _apicursoA.BuscarAsync();
            var sorted = from x in modelCurso
                         orderby x.Nome ascending
                         select x;

            foreach (CursoAcadescViewModel pai in sorted)
            {
                nodes.Add(new TreeViewNode { id = pai.Codigo.ToString(), parent = "#", text = pai.Nome });
            }

            var modelTurma = await _apiTurma.BuscarAsync();

            var sortedT = from x in modelTurma
                          orderby x.Nome ascending
                          select x;

            string[] turmaId = null;
            if (turma != null)
            {
                turmaId = turma.Split("|");
            }

            foreach (TurmaAcadescViewModel filho in sortedT)
            {
                var idNode = filho.Serie.ToString() + '.' + filho.Turma.ToString() + '.' + filho.Turno.ToString() + '.' + filho.Ciclo.ToString();
                Boolean selecionado = false;
                if (turma != null)
                {
                    foreach (var item in turmaId)
                    {
                        if (item == idNode)
                        {
                            selecionado = true;
                            break;
                        }
                    }
                    if (selecionado)
                    {
                        nodes.Add(new TreeViewNode { id = idNode, parent = filho.Ciclo.ToString(), text = filho.Nome + "Selecionado", selected = true });
                        selecionado = false;
                    }
                    else nodes.Add(new TreeViewNode { id = idNode, parent = filho.Ciclo.ToString(), text = filho.Nome, selected = false });

                }
                else
                {
                    nodes.Add(new TreeViewNode { id = idNode, parent = filho.Ciclo.ToString(), text = filho.Nome, selected = false });
                }
            }

            return nodes;
        }


    }
}
