using Genesis.Escola.MVC.HttpClients;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.ViewComponents
{
    public class Telefones : ViewComponent
    {
        private const char OldChar = 'b';
        private readonly ConfigApiClient _apiConfiguracao;

        public Telefones(ConfigApiClient apiConfiguracao)
        {
            _apiConfiguracao = apiConfiguracao;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetItemsAsync();
            model.Telefones = model.Telefones.Replace("</br>", " - ");
            var result = model.Telefones.Substring(model.Telefones.Length - 3);
            if (result == " - ") model.Telefones = model.Telefones.Remove(model.Telefones.Length - 3);
            return View(model);
        }


        //        public IViewComponentResult Invoke()

        private async Task<ConfigViewModel> GetItemsAsync()
        {
            var modelConfig = await _apiConfiguracao.BuscarAsync();
            return modelConfig;
        }
    }
}
