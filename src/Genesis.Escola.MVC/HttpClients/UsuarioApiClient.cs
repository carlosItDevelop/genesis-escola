using Genesis.Escola.MVC.Areas.Administrar.Models;
using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.HttpClients
{
    public class UsuarioApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public UsuarioApiClient(HttpClient client, IHttpContextAccessor accessor)
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

        public async Task<List<UsuarioViewModel>> BuscarAsync()
        {
            AddToken();
            var resposta = await _httpClient.GetAsync("v1/Auth/ObterTodos");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<UsuarioViewModel>>();
        }

        public async Task<UsuarioViewModel> BuscarAsync(string id)
        {
            AddToken();
            var resposta = await _httpClient.GetAsync($"v1/Auth/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<UsuarioViewModel>();
        }

        public async Task<LoginUserViewModel> BuscarAsync(LoginUserViewModel usuario)
        {
            var resposta = await _httpClient.PostAsJsonAsync("v1/Auth/login", usuario);
            //_httpClient.GetAsync($"v1/Auth/{curso}");
            resposta.EnsureSuccessStatusCode();

            var json = await resposta.Content.ReadAsStringAsync();
            var a = JsonConvert.DeserializeObject<JsonResult>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return await resposta.Content.ReadAsAsync<LoginUserViewModel>();
        }

        public async Task<LoginRetorno> obterLogin(LoginUserViewModel login)
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri("https://localhost:44321/api/");
            //curl -X GET "https://localhost:44321/api/v1/Aluno" -H "accept: application/json"
            httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("v1/Auth/login", login);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginRetorno>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        }

        #endregion

        #region Alterar
        public async Task AlterarAsync(string Id, UsuarioViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Auth/Alterar/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
