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
    public class ComunicadoApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public ComunicadoApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<List<ComunicadoViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Comunicado");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<ComunicadoViewModel>>();
        }

        public async Task<ComunicadoViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Comunicado/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<ComunicadoViewModel>();
        }

        public async Task<List<ComunicadoViewModel>> BuscarPorDataFinal(DateTime dataFim)
        {
            var resposta = await _httpClient.GetAsync($"v1/Comunicado/ObterPorDataFinal/{dataFim.ToString("yyyy-MM-dd")}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<ComunicadoViewModel>>();
        }

        public async Task<List<ComunicadoViewModel>> BuscarAtivas()
        {
            var resposta = await _httpClient.GetAsync("v1/Comunicado/ObterAtivos");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<ComunicadoViewModel>>();
        }

        public async Task<List<ComunicadoViewModel>> BuscarAtivas(string turma)
        {
            var resposta = await _httpClient.GetAsync($"v1/Comunicado/ObterAtivos/{turma}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<ComunicadoViewModel>>();
        }

        #endregion

        #region Incluir
        public async Task IncluirAsync(ComunicadoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<ComunicadoViewModel>("v1/Comunicado", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, ComunicadoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Comunicado/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Excluir
        public async Task ExcluirAsync(Guid id)
        {
            AddToken();
            var resposta = await _httpClient.DeleteAsync($"v1/Comunicado/{id}");
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

    }
}
