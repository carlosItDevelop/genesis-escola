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
    public class TurmaAcadescApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public TurmaAcadescApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<TurmaAcadescViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/TurmaAcadesc");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TurmaAcadescViewModel>>();
        }

        public async Task<TurmaAcadescViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/TurmaAcadesc/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<TurmaAcadescViewModel>();
        }

        public async Task<TurmaAcadescViewModel> BuscarAsync(string serie, string turma, string turno)
        {
            var resposta = await _httpClient.GetAsync($"v1/TurmaAcadesc/{serie}/{turma}/{turno}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<TurmaAcadescViewModel>();
        }

        public async Task<List<TurmaAcadescViewModel>> BuscarAsync(string ciclo)
        {
            var resposta = await _httpClient.GetAsync($"v1/TurmaAcadesc/{ciclo}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TurmaAcadescViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(TurmaAcadescViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<TurmaAcadescViewModel>("v1/TurmaAcadesc", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, TurmaAcadescViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/TurmaAcadesc/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/TurmaAcadesc/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
