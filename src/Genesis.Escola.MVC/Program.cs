using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Genesis.Escola.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var caminhoLog = Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");

            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                        .Enrich.FromLogContext()
                        .WriteTo.File(caminhoLog)
                        .CreateLogger();

            try
            {
                Log.Information("-----> Iniciando o Servidor <------");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "-----> Servidor Terminou Inesperadamente <------");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            // CurrentDirectoryHelpers.SetCurrentDirectory();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();


    }
}
