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
    public class CronogramaApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public CronogramaApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<CronogramaViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Cronograma");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CronogramaViewModel>>();
        }

        public async Task<CronogramaViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Cronograma/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<CronogramaViewModel>();
        }

        public async Task<List<CronogramaViewModel>> BuscarPorDataFinal(DateTime dataFim)
        {
            var resposta = await _httpClient.GetAsync($"v1/Cronograma/ObterPorDataFinal/{dataFim.ToString("yyyy-MM-dd")}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CronogramaViewModel>>();
        }

        public async Task<List<CronogramaViewModel>> BuscarAtivas()
        {
            var resposta = await _httpClient.GetAsync("v1/Cronograma/ObterAtivos");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CronogramaViewModel>>();
        }

        public async Task<List<CronogramaViewModel>> BuscarAtivas(string turma)
        {
            var resposta = await _httpClient.GetAsync($"v1/Cronograma/ObterAtivos/{turma}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CronogramaViewModel>>();
        }


        #endregion

        #region Incluir
        public async Task IncluirAsync(CronogramaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<CronogramaViewModel>("v1/Cronograma", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, CronogramaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Cronograma/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/Cronograma/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
