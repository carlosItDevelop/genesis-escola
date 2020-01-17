using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Areas.Administrar.Controllers
{
    public class ComunicadoController : Controller
    {
        #region Variaveis
        private readonly ComunicadoApiClient _api;
        private readonly CursoAcadescApiClient _apicursoA;
        private readonly TurmaAcadescApiClient _apiTurma;
        private IConfiguration _configuration;
        #endregion

        #region Construtor
        public ComunicadoController(ComunicadoApiClient api,
                                    TurmaAcadescApiClient apiTurma,
                                    CursoAcadescApiClient apicursoA,
                                    IConfiguration configuration)
        {
            _api = api;
            _apiTurma = apiTurma;
            _apicursoA = apicursoA;
            _configuration = configuration;
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
            ViewBag.Json = JsonConvert.SerializeObject(await BuscarTurmaGeral());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Adicionar(ComunicadoViewModel model, IFormFile file, string selectedItems)
        {

            List<TreeViewNode> itemsRetornados = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItems);
            var turma = "";

            foreach (var item in itemsRetornados)
            {
                if (item.id.Length > 3) turma += item.id + '|';
            }

            ViewBag.Json = JsonConvert.SerializeObject(await BuscarTurmaGeral());

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

            ViewData["CaminhoImagem"] = _configuration["UrlApi:WebApiBaseUrl"] + "v1/Comunicados/PegarPdf/" + id;
            var model = await _api.BuscarAsync(id);

            // var turmaId = model.TurmaId.Split("|");
            //  var teste = await GetTreeData(model.TurmaId);
            var teste = await GetTreeData3();

            //var teste = await BuscarTurmaGeral(model.TurmaId);
            //var treeCiclo = await BuscarTurmaGeral(model.TurmaId);

            ViewBag.Json = JsonConvert.SerializeObject(teste);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Area("Administrar")]
        public async Task<ActionResult> Editar(Guid id, ComunicadoViewModel model, IFormFile file, string selectedItems)
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
            var model = await _api.BuscarAsync(id);
            return View(model);
        }


        [Area("Administrar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Excluir(Guid id, ComunicadoViewModel dados)
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
                        nodes.Add(new TreeViewNode { id = idNode, parent = filho.Ciclo.ToString(), text = filho.Nome + " Selecionado", selected = true, state = "state : { checked : true }" });
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

        private async Task<List<JsTreeModel>> GetTreeData(string turma = null)
        {
            var modelCurso = await _apicursoA.BuscarAsync();
            var sorted = from x in modelCurso
                         orderby x.Nome ascending
                         select x;
            List<JsTreeModel> nodes = new List<JsTreeModel>();

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
                    //buscar filhos
                    JsTreeModel[] filhos = new JsTreeModel[turmas.Count()];
                    int i = 0;
                    foreach (var filho in turmas)
                    {
                        var idNode = filho.Serie.ToString() + '.' + filho.Turma.ToString() + '.' + filho.Turno.ToString() + '.' + filho.Ciclo.ToString();
                        //  filhos[i] = new JsTreeModel { text = filho.Nome, id = idNode, parent = idNode, attr = new JsTreeAttribute { id = filho.Id.ToString(), selected = true } };
                        i++;
                    }

                    nodes.Add(new JsTreeModel
                    {
                        text = pai.Nome,
                        parent = "#",
                        //   attr = new JsTreeAttribute { id = pai.Codigo.ToString(), selected = false },
                        //children = filhos
                    });
                }
                else
                {
                    nodes.Add(new JsTreeModel
                    {
                        text = pai.Nome,
                        parent = "#",
                        //   attr = new JsTreeAttribute { id = pai.Codigo.ToString(), selected = false }
                    }); ;
                }

            }


            return nodes;
        }

        private async Task<List<JsTreeRoot>> GetTreeData3()
        {
            var modelCurso = await _apicursoA.BuscarAsync();
            var sorted = from x in modelCurso
                         orderby x.Nome ascending
                         select x;
            List<JsTreeRoot> nodes = new List<JsTreeRoot>();

            List<JsTreeModel> child = new List<JsTreeModel>();

            JsTreeModel[] treeModel = new JsTreeModel[10000];

            foreach (CursoAcadescViewModel pai in sorted)
            {
                var turmas = await _apiTurma.BuscarAsync(pai.Codigo);
                if (turmas.Count() > 0)
                {

                    JsTreeChildren[] filhos = new JsTreeChildren[turmas.Count()];
                    int i = 0;
                    foreach (var filho in turmas)
                    {
                        var idNode = filho.Serie.ToString() + '.' + filho.Turma.ToString() + '.' + filho.Turno.ToString() + '.' + filho.Ciclo.ToString();
                        filhos[i] = new JsTreeChildren { text = filho.Nome, id = idNode, state = new JsTreeState { opened = true, selected = true, disabled = false } };
                        i++;
                    }


                    child.Add(new JsTreeModel
                                {
                                  text = pai.Nome,
                                  id = pai.Codigo,
                                  parent ="#",
                                  children= filhos 
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

        private JsTreeRoot[] GetTreeData2()
        {
            var tree = new JsTreeRoot[]
            {
                new JsTreeRoot
                {
                    text ="Todas as Turmas",
                    children = new JsTreeModel[]
                    {
                        new JsTreeModel
                        {
                            text = "Things1", id="20",
                            state = new JsTreeState { opened = true, selected = false , disabled=false},
                            parent="#",
                            children= new JsTreeChildren[]
                            {
                               // id="1'",text="lllllllll",
                               new JsTreeChildren {
                                   id="1",
                                   text="jjjjjj",
                                   state = new JsTreeState { opened = true, selected = true , disabled=false},
                                   
                               },
                               new JsTreeChildren {
                                   id="12",
                                   text="2222222222jjjjjj",
                                   state = new JsTreeState { opened = true, selected = false , disabled=false},

                               }
                            }
                        }
                    }
                }
            };
            return tree;
        }

        private JsTreeModel[] GetTreeData1()
        {



            var tree = new JsTreeModel[]
            {
               // new JsTreeModel { text = "Confirm Application",id="10",parent="#",  state = new JsTreeState { opened = true, selected = true , disabled=false}},
                new JsTreeModel
                {
                    text = "Things", id="20",
                     state = new JsTreeState { opened = true, selected = true , disabled=false},
                    parent="#",
                    children= new JsTreeChildren[]
                    {
                       // id="1'",text="lllllllll",
                       // new JsTreeChildren {id="1",text="jjjjjj"}
                    }
                    //"text : 'Child 1' ," + "id:'21'",
                    // children = new JsTreeModel[]
                   //     {
                    //        new JsTreeModel { text = "Thing 1", id="21",parent="#"},
                    //        new JsTreeModel { text = "Thing 2", id="22",parent="#", state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //        new JsTreeModel { text = "Thing 3", id="23",parent="#", state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //        new JsTreeModel
                    //        {
                    //            text = "Thing 4",id="24",
                    //            state = new JsTreeState { opened = true, selected = true , disabled=false},
                    //            parent="#",
                    //            children = new JsTreeModel[]
                    //            {
                    //                new JsTreeModel { text = "Thing 4.1",id="241",parent="#",  state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //                new JsTreeModel { text = "Thing 4.2",id="242", parent="#",state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //                new JsTreeModel { text = "Thing 4.3",id="243",parent="#", state = new JsTreeState { opened = true, selected = true , disabled=false} }
                    //            },
                            //},
                     //   }
                },
                new JsTreeModel
                {
                    text = "Colors",
                    id="40",
                    state = new JsTreeState { opened = true, selected = true , disabled=false},parent="#"
                    //children = new JsTreeModel[]
                    //    {
                    //        new JsTreeModel { text = "Red", id="41",parent="#",state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //        new JsTreeModel { text = "Green", id="42",parent="#",state = new JsTreeState { opened = true, selected = true , disabled=false}},
                    //        new JsTreeModel { text = "Blue", id="43",parent="#",state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //        new JsTreeModel { text = "Yellow",id="44", parent="#",state = new JsTreeState { opened = true, selected = true , disabled=false} },
                    //    }
                }
            };

            return tree;
        }

    }
}