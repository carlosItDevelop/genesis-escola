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
    public class GaleriaItemApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public GaleriaItemApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<GaleriaItensViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/GaleriaItem");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<GaleriaItensViewModel>>();
        }

        public async Task<GaleriaItensViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/GaleriaItem/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<GaleriaItensViewModel>();
        }

        public async Task<List<GaleriaItensViewModel>> BuscarTodosPorGaleria(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/GaleriaItem/ObterTodosPorGaleria/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<GaleriaItensViewModel>>();
        }
        #endregion

        #region Incluir
        public async Task IncluirAsync(GaleriaItensViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<GaleriaItensViewModel>("v1/GaleriaItem", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
