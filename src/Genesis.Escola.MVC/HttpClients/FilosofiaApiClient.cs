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
    public class FilosofiaApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public FilosofiaApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<FilosofiaViewModel> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Filosofia");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<FilosofiaViewModel>();
        }
        #endregion

        #region Incluir
        public async Task IncluirAsync(FilosofiaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<FilosofiaViewModel>("v1/Filosofia", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, FilosofiaViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Filosofia/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
