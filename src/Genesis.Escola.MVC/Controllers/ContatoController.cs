using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Genesis.Escola.MVC.services;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MimeKit.Text;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.Controllers
{
    public class ContatoController : Controller
    {
        #region Variaveis
        private readonly ConfigApiClient _api;
        private readonly EmailContatoApiClient _apiEmail;
        private readonly IEmailService _emailService;
        #endregion

        #region Construtor
        public ContatoController(ConfigApiClient api,
            EmailContatoApiClient apiEmail, IEmailService emailService)
        {
            _api = api;
            _emailService = emailService;
            _apiEmail = apiEmail;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _api.BuscarAsync();
            if (model == null) model = new ConfigViewModel();
            ViewBag.Telefone = model.Telefones;
            ViewBag.Endereco = model.Endereco;
            ViewBag.TituloContato = model.TituloContato;
            ViewBag.DescricaoContato = model.DescricaoContato;
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = "Selecione um Departamento",
                Value = ""
            });

            var modelEmailDepto = await _apiEmail.BuscarAsync();
            foreach (var Linha in modelEmailDepto)
            {
                Lista.Add(new SelectListItem()
                {
                    Value = Linha.Email.ToString(),
                    Text = Linha.Nome,
                });
            }
            ViewBag.Selecionador = Lista;

            var modelEmail = new Emails();


            return View(modelEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Emails emailsViewModel)
        {
            if (ModelState.IsValid)
            {
                var modelConfig = await _api.BuscarAsync();
                await _emailService.SendEmail(emailsViewModel.Email, emailsViewModel.Nome, emailsViewModel.Assunto,emailsViewModel.Mensagem,emailsViewModel.EmailPara, modelConfig, EnumTipoEmail.Contato);
                return RedirectToAction(nameof(Recebido));
            }
            var model = await _api.BuscarAsync();
            if (model == null) model = new ConfigViewModel();
            ViewBag.Telefone = model.Telefones;
            ViewBag.Endereco = model.Endereco;
            ViewBag.TituloContato = model.TituloContato;
            ViewBag.DescricaoContato = model.DescricaoContato;
            List<SelectListItem> Lista = new List<SelectListItem>();
            Lista.Add(new SelectListItem //adiciona uma opção que convida a escolher uma das possíveis opções
            {
                Text = "Selecione um Departamento",
                Value = ""
            });

            var modelEmailDepto = await _apiEmail.BuscarAsync();
            foreach (var Linha in modelEmailDepto)
            {
                Lista.Add(new SelectListItem()
                {
                    Value = Linha.Email.ToString(),
                    Text = Linha.Nome,
                });
            }
            ViewBag.Selecionador = Lista;
            return View(emailsViewModel);
        }

        #endregion

        #region Trabalhe Conosco
        public async Task<IActionResult> Trabalhe()
        {
            var model = await _api.BuscarAsync();
            if (model == null) model = new ConfigViewModel();

            ViewBag.Telefone = model.Telefones;
            ViewBag.Endereco = model.Endereco;
            ViewBag.TituloTrabalhe = model.TituloTrabalhe;
            ViewBag.DescricaoTrabalhe = model.DescricaoTrabalhe;
            var modelEmail = new EmailTrabalhe();
            return View(modelEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Trabalhe(EmailTrabalhe emailsViewModel, IFormFile file)
        {

            if (file == null)
            {
                ModelState.AddModelError("Attachment", "O Curriculum é obrigatório");
            }
            else if (file.Length == 0)
            {
                ModelState.AddModelError("Attachment", "O Curriculum é obrigatório");
            }
            else if (file.Length > 2009393)
            {
                ModelState.AddModelError("Attachment", "O Arquivo é maior que 2 Mb");
            }

       
            if (ModelState.IsValid)
            {
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                if (fileExt != ".pdf" && fileExt != ".doc" && fileExt != ".docx")
                {
                    ModelState.AddModelError("Attachment", "Só são aceitos arquivos pdf,doc ou docx");
                }
            }

            if (ModelState.IsValid)
            {
                var modelConfig = await _api.BuscarAsync();
                await _emailService.SendEmail(emailsViewModel.Email, emailsViewModel.Nome, "", emailsViewModel.Mensagem,modelConfig.EmailRetTrabalhe, modelConfig, EnumTipoEmail.TrabalheConosco,file);
                return RedirectToAction(nameof(Recebido));
            }
            return View(emailsViewModel);
        }

        #endregion

        #region Recebido
        public ActionResult Recebido()
        {
            return View();
        }
        #endregion

    }
}