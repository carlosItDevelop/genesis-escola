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
    public class CursosApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public CursosApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<CursoViewModel> BuscarAsync(EnumCurso curso)
        {
            var resposta = await _httpClient.GetAsync(string.Format("v1/Cursos?curso={0}", curso.GetHashCode()));
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<CursoViewModel>();
        }
        #endregion

        #region Incluir
        public async Task IncluirAsync(CursoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<CursoViewModel>("v1/Cursos", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, CursoViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Cursos/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
