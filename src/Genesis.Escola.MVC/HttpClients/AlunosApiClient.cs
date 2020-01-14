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
    public class AlunosApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _acessor;
        #endregion

        #region Construtor
        public AlunosApiClient(HttpClient client, IHttpContextAccessor accessor)
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
        public async Task<AlunosViewModel> LogarAsync(string matricula, string senha)
        {
            var resposta = await _httpClient.GetAsync(string.Format("v1/Aluno/{0}/{1}", matricula, senha));
            if (resposta.IsSuccessStatusCode) return await resposta.Content.ReadAsAsync<AlunosViewModel>();
            else return null;
        }

        public async Task<AlunosViewModel> BuscarPorMatricula(string matricula)
        {
            var resposta = await _httpClient.GetAsync(string.Format("v1/Aluno/{0}", matricula));
            if (resposta.IsSuccessStatusCode) return await resposta.Content.ReadAsAsync<AlunosViewModel>();
            else return null;
        }

        public async Task<List<AlunosViewModel>> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Aluno");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<List<AlunosViewModel>>();
        }

        public async Task<AlunosViewModel> BuscarAsync(Guid id)
        {
            var resposta = await _httpClient.GetAsync($"v1/Aluno/{id}");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<AlunosViewModel>();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, AlunosViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Aluno/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

    }
}
