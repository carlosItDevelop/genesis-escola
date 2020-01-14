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
    public class PoloApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public PoloApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<PoloViewModel> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Polo");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<PoloViewModel>();
        }
        #endregion

        #region Incluir
        public async Task IncluirAsync(PoloViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<PoloViewModel>("v1/Polo", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, PoloViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Polo/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
