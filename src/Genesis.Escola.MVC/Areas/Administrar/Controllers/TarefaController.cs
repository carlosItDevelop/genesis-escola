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
    public class TarefaController : Controller
    {
        #region Variaveis
        private readonly TarefaApiClient _api;
        private readonly TurmaAcadescApiClient _apiTurma;
        private readonly CursoAcadescApiClient _apicursoA;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public TarefaController(TarefaApiClient api, 
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
            ViewBag.Json = JsonConvert.SerializeObject(await BuscarTurma());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Adicionar(TarefaViewModel model, IFormFile file, string selectedItems)
        {
            var turma = "";
            if (!string.IsNullOrEmpty(selectedItems))
            {
                List<TreeViewNode> itemsRetornados = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
                foreach (var item in itemsRetornados)
                {
                    if (item.id.Length > 3) turma += item.id + '|';
                }
            }
            else ModelState.AddModelError("", "Deve Selecionar pelo menos uma turma");

            ViewBag.Json = JsonConvert.SerializeObject(await BuscarTurma());

            if (file != null && file.Length > 0)
            {
                if (file.Length > 2009393)
                {
                    ModelState.AddModelError("", "O Arquivo é maior que 2 Mb");
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
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Tarefa/PegarPdf/" + id;
            var model = await _api.BuscarAsync(id);
            model.selectedItems = model.TurmaId;

            var turmas = await BuscarTurma(model.TurmaId);

            ViewBag.Json = JsonConvert.SerializeObject(turmas);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id, TarefaViewModel model, IFormFile file, string selectedItems)
        {

            var turma = "";
            if (!string.IsNullOrEmpty(selectedItems))
            {
                List<TreeViewNode> itemsRetornados = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
                foreach (var item in itemsRetornados)
                {
                    if (item.id.Length > 3) turma += item.id + '|';
                }
            }
            else ModelState.AddModelError("", "Deve Selecionar pelo menos uma turma");

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
                model.TurmaId = turma;
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
            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Tarefa/PegarPdf/" + id;
            var model = await _api.BuscarAsync(id);
            return View(model);
        }


        [Area("Administrar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id, TarefaViewModel dados)
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

        #region Buscar Turma

        private async Task<List<JsTreeRoot>> BuscarTurma(string turma = null)
        {
            var modelCurso = await _apicursoA.BuscarAsync();
            var sorted = from x in modelCurso
                         orderby x.Nome ascending
                         select x;
            List<JsTreeRoot> nodes = new List<JsTreeRoot>();

            List<JsTreeModel> child = new List<JsTreeModel>();

            JsTreeModel[] treeModel = new JsTreeModel[10000];

            string[] turmaId = null;
            if (turma != null)
            {
                turmaId = turma.Split("|");
            }


            foreach (CursoAcadescViewModel pai in sorted)
            {
                var turmas = await _apiTurma.BuscarAsync(pai.Codigo);
                if (turmas.Count() > 0)
                {
                    JsTreeChildren[] filhos = new JsTreeChildren[turmas.Count()];
                    int i = 0;
                    foreach (var filho in turmas)
                    {
                        var idNode = filho.Serie.ToString() + '-' + filho.Turma.ToString() + '-' + filho.Turno.ToString() + '-' + filho.Ciclo.ToString();
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
                        }
                        filhos[i] = new JsTreeChildren { text = filho.Nome, id = idNode, state = new JsTreeState { opened = true, selected = selecionado, disabled = false } };
                        i++;
                    }


                    child.Add(new JsTreeModel
                    {
                        text = pai.Nome,
                        id = pai.Codigo,
                        parent = "#",
                        children = filhos
                    }
                    );
                }
                else
                {
                    child.Add(new JsTreeModel
                    {
                        text = pai.Nome,
                        id = pai.Codigo,
                        parent = "#"
                    }
                    );
                }
            }
            treeModel = child.ToArray();

            nodes.Add(
                          new JsTreeRoot
                          {
                              text = "Todas as Turmas",
                              children = treeModel
                          }
                );

            return nodes;
        }

        #endregion

    }
}