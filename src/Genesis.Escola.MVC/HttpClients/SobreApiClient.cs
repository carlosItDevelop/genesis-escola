using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.HttpClients
{
    public class SobreApiClient : MainApiClient
    {
        #region Variaveis
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpAccessor;
        //private readonly HttpRequest _httpRequest;
        //private readonly HttpResponse _httpResponse;
        #endregion

        #region Construtor
        public SobreApiClient(HttpClient httpClient, IHttpContextAccessor httpAccessor) : base(httpClient,httpAccessor) //: base(accessor,client, httpRequest, httpResponse)
        {
            _httpClient = httpClient;
            //_httpRequest = httpRequest;
            //_httpResponse = httpResponse;
            _httpAccessor = httpAccessor;
        }
        #endregion

        #region Buscar
        public async Task<SobreViewModel> BuscarAsync()
        {
            var resposta = await _httpClient.GetAsync("v1/Sobre");
            resposta.EnsureSuccessStatusCode();
            return await resposta.Content.ReadAsAsync<SobreViewModel>();
        }
        #endregion

        #region Incluir
        public async Task IncluirAsync(SobreViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PostAsJsonAsync<SobreViewModel>("v1/Sobre", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion

        #region Alterar
        public async Task AlterarAsync(Guid Id, SobreViewModel model)
        {
            AddToken();
            var resposta = await _httpClient.PutAsJsonAsync($"v1/Sobre/{Id}", model);
            resposta.EnsureSuccessStatusCode();
        }
        #endregion
    }
}
