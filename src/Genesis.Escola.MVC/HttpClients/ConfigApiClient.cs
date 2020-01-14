using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.HttpClients
{
    public class ConfigApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public ConfigApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<ConfigViewModel> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync(string.Format("v1/Config" ));
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<ConfigViewModel>();
        }
        #endregion

        #region Incluir
        public async Task<RetornoApi> IncluirAsync(ConfigViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<ConfigViewModel>("v1/Config", model);
            var resultado = await resposta.Content.ReadAsStringAsync();

            var reader = new JsonTextReader(new StringReader(resultado));
            var serializer = new JsonSerializer();
            var obj = serializer.Deserialize<RetornoApi>(reader);
            if (obj == null)
            {
                obj = new RetornoApi();
                obj.Success = resposta.IsSuccessStatusCode;
                obj.errors = new string[1] { resposta.StatusCode.ToString()}; 
            }
            return obj;
        }
        #endregion

        #region Alterar
        public async Task<RetornoApi> AlterarAsync(Guid Id, ConfigViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Config/{Id}", model);
            var resultado = await resposta.Content.ReadAsStringAsync();

            var reader = new JsonTextReader(new StringReader(resultado));
            var serializer = new JsonSerializer();
            var obj = serializer.Deserialize<RetornoApi>(reader);
            if (obj == null)
            {
                obj = new RetornoApi();
                obj.Success = resposta.IsSuccessStatusCode;
                obj.errors = new string[1] { resposta.StatusCode.ToString() };
            }
            return obj;
        }
        #endregion
    }
}
