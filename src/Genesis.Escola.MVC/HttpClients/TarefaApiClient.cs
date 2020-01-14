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
    public class TarefaApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public TarefaApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<TarefaViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Tarefa");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TarefaViewModel>>();
        }

        public async Task<TarefaViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Tarefa/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<TarefaViewModel>();
        }

        public async Task<List<TarefaViewModel>> BuscarPorDataFinal(DateTime dataFim)
        {
            var resposta = await _httpClient.GetAsync($"v1/Tarefa/ObterPorDataFinal/{dataFim.ToString("yyyy-MM-dd")}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TarefaViewModel>>();
        }

        public async Task<List<TarefaViewModel>> BuscarAtivas()
        {
            var resposta = await _httpClient.GetAsync("v1/Tarefa/ObterAtivos");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TarefaViewModel>>();
        }

        public async Task<List<TarefaViewModel>> BuscarAtivas(string turma)
        {
            var resposta = await _httpClient.GetAsync($"v1/Tarefa/ObterAtivos/{turma}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<TarefaViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(TarefaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<TarefaViewModel>("v1/Tarefa", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, TarefaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Tarefa/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/Tarefa/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
