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
    public class NoticiasApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public NoticiasApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<NoticiasViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Noticias");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<NoticiasViewModel>>();
        }

        public async Task<NoticiasViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Noticias/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<NoticiasViewModel>();
        }

        public async Task<List<NoticiasViewModel>> BuscarPorDataFinal(DateTime dataFim)
        {
            var resposta = await _httpClient.GetAsync($"v1/Noticias/ObterPorDataFinal/{dataFim.ToString("yyyy-MM-dd")}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<NoticiasViewModel>>();
        }

        public async Task<List<NoticiasViewModel>> BuscarAtivas()
        {
            var resposta = await _httpClient.GetAsync("v1/Noticias/ObterAtivas");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<NoticiasViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(NoticiasViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<NoticiasViewModel>("v1/Noticias", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, NoticiasViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Noticias/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/noticias/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

    }
}
