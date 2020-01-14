using Genesis.Escola.MVC.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Functions
{
    public class ConsumirWebApi
    {
        #region Variaveis
        private static string URI = ConfigurationManager.AppSetting["UrlApi:WebApiBaseUrl"];
        private HttpContext _context;
        
        public static string TOKEN = ""; //Consumir quando tiver Token

        #endregion

        //ToDo: Criar uma função static que retorna o Token do usuario logado
        //ToDo: ou o usuario Logado

        public ConsumirWebApi(HttpContext context)
        {
            _context = context;
            TOKEN = _context.User.Claims.First(c => c.Type == "Token").Value;
        }

        #region Get na APi
        public static async Task<string> RequestGET(string metodo, string parametro)
        {

            var request = (HttpWebRequest)WebRequest.Create(URI + metodo + "/" + parametro);
            //  request.Headers.Add("Token", TOKEN);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        #endregion

        #region Get na APi
        public async Task<string> RequestGET1(string metodo, string parametro)
        {

            var request = (HttpWebRequest)WebRequest.Create(URI + metodo + "/" + parametro);
            request.Headers.Add("Token", TOKEN);
           // _context.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Method = "GET";
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
        #endregion

        //public static async Task<string> RequestDELETE(string metodo, string parametro)
        //{
        //    return await RequesteGET_DELETE(metodo, parametro, "DELETE");
        //}

        public static async Task<string> RequestDELETE(string metodo, string Id)
        {
            using (var client = new HttpClient())
            {
                var resultado = client.DeleteAsync(URI + metodo + "/" + Id).Result;
                string mensagem = "OK";
                if (!resultado.IsSuccessStatusCode)
                {
                    mensagem = "Erro ao Excluir";
                }
                return mensagem;
            }
        }

        #region Post na Api
        public static async Task<Message<T>> RequestPOST<T>(string metodo, T content)
        {
            using (var client = new HttpClient())
            {
                Message<T> mensagem = new Message<T>();

                client.BaseAddress = new Uri(URI + metodo);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // AddBearerToken a = new AddBearerToken();
                //var token= _context.User.Claims // _acessor.HttpContext.Request.Cookies["AspNetCore.Cookies"].ToString();
                // var token = User.Claims.First(c => c.Type == "Token").Value;
             //   var token = _context.User.Identity.Name;               //var token = a.BearerToken();
               //client.DefaultRequestHeaders.Add("Authorization", "bearer " + a.BearerToken());

                var resultado = await client.PostAsJsonAsync(client.BaseAddress.ToString(), content);
                if (resultado.IsSuccessStatusCode)
                {
                    mensagem.IsSuccess = true;
                    var mensJson = await resultado.Content.ReadAsStringAsync();
                    string[] messagem = { JsonConvert.DeserializeObject(mensJson).ToString() };
                    mensagem.ReturnMessage = messagem;
                }
                else
                {
                    mensagem.IsSuccess = false;
                    mensagem.ReturnMessage = JsonConvert.DeserializeObject(resultado.Content.ReadAsStringAsync().Result).ToString().Split("\n");
                }
                return mensagem;
            }
        }
        #endregion

        public static async Task<Message<T>> RequestPUT<T>(string metodo, T content)
        {
            using (var client = new HttpClient())
            {
                Message<T> mensagem = new Message<T>();

                client.BaseAddress = new Uri(URI + metodo);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //AddBearerToken a = new AddBearerToken();

                //var token = a.BearerToken();
                //client.DefaultRequestHeaders.Add("Authorization", "bearer " + a.BearerToken());
                string jsonData = JsonConvert.SerializeObject(content);
                var resultado = await client.PutAsJsonAsync(client.BaseAddress.ToString(), content);
                if (resultado.IsSuccessStatusCode)
                {
                    string[] messagem = { "OK" };
                    mensagem.IsSuccess = true;
                    mensagem.ReturnMessage = messagem;
                }
                else
                {
                    mensagem.IsSuccess = false;
                    mensagem.ReturnMessage = JsonConvert.DeserializeObject(resultado.Content.ReadAsStringAsync().Result).ToString().Split("\n");
                }
                return mensagem;
            }
        }

        public string BuscarToken()
        {

            return "";
        }
    }
}
