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
    public class TurmaApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public TurmaApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<TurmaViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Turma");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TurmaViewModel>>();
        }

        public async Task<TurmaViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Turma/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<TurmaViewModel>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(TurmaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<TurmaViewModel>("v1/Turma", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, TurmaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Turma/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/Turma/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
