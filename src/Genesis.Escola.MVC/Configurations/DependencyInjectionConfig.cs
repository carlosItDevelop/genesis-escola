using Genesis.Escola.MVC.HttpClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<SobreApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<NoticiasApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<PoloApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<MissaoApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<FilosofiaApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<CursosApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<GaleriaApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<GaleriaItemApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<UsuarioApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<CarrosselApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<AlunosApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<ComunicadoApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<TurmaApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<ConfigApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<EmailContatoApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<TurmaAcadescApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<TarefaApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<CronogramaApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<NotasApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            services.AddHttpClient<CursoAcadescApiClient>(client => {
                client.BaseAddress = new Uri(configuration["UrlApi:WebApiBaseUrl"]);
            });
            return services;
        }
    }
}
