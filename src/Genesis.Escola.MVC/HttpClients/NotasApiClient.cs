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
    public class NotasApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public NotasApiClient(HttpClient client, IHttpContextAccessor accessor)
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

        public async Task<List<NotasViewModel>> BuscarBoletim(string aluno,string senha)
        {
            var resposta = await _httpClient.GetAsync($"v1/Notas/ObterPorAluno/{aluno}/{senha}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<NotasViewModel>>();
        }

        #endregion



    }
}
