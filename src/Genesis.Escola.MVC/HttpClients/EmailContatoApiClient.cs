using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.HttpClients
{
    public class EmailContatoApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public EmailContatoApiClient(HttpClient client, IHttpContextAccessor accessor)
        {
            _httpClient = client;
            _acessor = accessor;
        }
        #endregion

        #region Pegar o Token
        private void AddToken()
        {
            var token = _acessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion

        #region Buscar
        public async Task<List<EmailContatoViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/EmailContato");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<EmailContatoViewModel>>();
        }

        public async Task<EmailContatoViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/EmailContato/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<EmailContatoViewModel>();
        }

        public async Task<List<EmailContatoViewModel>> BuscarPorDataFinal(DateTime dataFim)
        {
            var resposta = await _httpClient.GetAsync($"v1/EmailContato/ObterPorDataFinal/{dataFim.ToString("yyyy-MM-dd")}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<EmailContatoViewModel>>();
        }

        public async Task<List<EmailContatoViewModel>> BuscarAtivas()
        {
            var resposta = await _httpClient.GetAsync("v1/EmailContato/ObterAtivas");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<EmailContatoViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(EmailContatoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<EmailContatoViewModel>("v1/EmailContato", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, EmailContatoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/EmailContato/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/EmailContato/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
