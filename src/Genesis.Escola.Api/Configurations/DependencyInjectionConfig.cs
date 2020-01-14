using Genesis.Escola.Api.Extensions;
using Genesis.Escola.Business.Interfaces;
using Genesis.Escola.Business.Interfaces.Services;
using Genesis.Escola.Business.Notificacoes;
using Genesis.Escola.Business.Services;
using Genesis.Escola.Data.Context;
using Genesis.Escola.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Genesis.Escola.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<Contexto>();
            services.AddScoped<INoticiaRepositorio, NoticiaRepositorio>();
            services.AddScoped<IMissaoRepositorio, MissaoRepositorio>();
            services.AddScoped<IPoloRepositorio, PoloRepositorio>();
            services.AddScoped<ISobreRepositorio, SobreRepositorio>();
            services.AddScoped<IFilosofiaRepositorio, FilosofiaRepositorio>();
            services.AddScoped<ICursosRepositorio, CursosRepositorio>();
            services.AddScoped<ICursoAcadescRepositorio, CursoAcadescRepositorio>();
            services.AddScoped<IGaleriaRepositorio, GaleriaRepositorio>();
            services.AddScoped<IGaleriaItensRepositorio, GaleriaItensRepositorio>();
            services.AddScoped<ICarrosselRepositorio, CarrosselRepositorio>();
            services.AddScoped<IAlunoRepositorio, AlunoRepositorio>();
            services.AddScoped<IComunicadoRepositorio, ComunicadoRepositorio>();
            services.AddScoped<ITurmaRepositorio, TurmaRepositorio>();
            services.AddScoped<IConfigRepositorio, ConfigRepositorio>();
            services.AddScoped<IEmailContatoRepositorio, EmailContatoRepositorio>();
            services.AddScoped<ITurmaAcadescRepositorio, TurmaAcadescRepositorio>();
            services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
            services.AddScoped<ICronogramaRepositorio, CronogramaRepositorio>();
            services.AddScoped<INotasRepositorio, NotasRepositorio>();
            services.AddScoped<IDisciplinasRepositorio, DisciplinasRepositorio>();

            services.AddScoped<INotificador, Notificador>();


            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddScoped<IMissaoService, MissaoService>();
            services.AddScoped<IPoloService, PoloService>();
            services.AddScoped<ISobreService, SobreService>();
            services.AddScoped<IFilosofiaService, FilosofiaService>();
            services.AddScoped<ICursoService, CursoService>();
            services.AddScoped<ICursoAcadescService, CursoAcadescService>();
            services.AddScoped<IGaleriaService, GaleriaService>();
            services.AddScoped<IGaleriaItensService, GaleriaItemService>();
            services.AddScoped<ICarrosselService, CarrosselService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IComunicadoService, ComunicadoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IEmailContatoService, EmailContatoService>();
            services.AddScoped<ITurmaAcadescService, TurmaAcadescService>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<ICronogramaService, CronogramaService>();
            services.AddScoped<INotasService, NotasService>();
            services.AddScoped<IDisciplinasService, DisciplinasService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
