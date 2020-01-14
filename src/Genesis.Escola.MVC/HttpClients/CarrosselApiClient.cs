using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Genesis.Escola.MVC.Functions.Enumeradores;

namespace Genesis.Escola.MVC.HttpClients
{
    public class CarrosselApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public CarrosselApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<CarrosselViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Carrossel");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CarrosselViewModel>>();
        }

        public async Task<CarrosselViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Carrossel/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<CarrosselViewModel>();
        }

        public async Task<List<CarrosselViewModel>> BuscarAtivoInativo(EnumAtivoInativo tipo)
        {
            var resposta = await _httpClient.GetAsync($"v1/Carrossel/ObterAtivoInativo/{tipo}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<CarrosselViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(CarrosselViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<CarrosselViewModel>("v1/Carrossel", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, CarrosselViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Carrossel/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/Carrossel/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
