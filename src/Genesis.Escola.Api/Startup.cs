using AutoMapper;
using Genesis.Escola.Api.Configurations;
using Genesis.Escola.Api.Data;
using Genesis.Escola.Api.Migrations;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;

namespace Genesis.Escola.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContext<Contexto>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("ApiConnection")));

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                .AllowAnyMethod()
                                                                 .AllowAnyHeader()));

            services.AddDbContext<Contexto>(options =>
                    options.UseMySql(Configuration.GetConnectionString("ApiConnectionMy")));

            services.AddIdentityConfiguration(Configuration);

            services.AddAutoMapper(typeof(Startup));

            services.WebApiConfig();

            services.AddSwaggerConfig();

            services.ResolveDependencies();
        }

        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IApiVersionDescriptionProvider provider,
                              ApplicationDbContext _contexto,
                              UserManager<UsuarioViewModel> userManager,
                              RoleManager<IdentityRoleViewModel> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowAll");

            app.UseAuthentication();

            DbInitializer.Iniciar(_contexto, userManager, roleManager);

            app.UseMvcConfiguration();

            app.UseSwaggerConfig(provider);

        }
    }
}
