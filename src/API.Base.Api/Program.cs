using System;
using API.Base.Api.Extensions.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Base.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var application = typeof(Program).Assembly.ManifestModule.Name;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", application)
                .Enrich.WithProperty("Environment", env)
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            Log.Logger.Information($"{application} started.");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureAppConfiguration((hosting, config) => { config.AddJsonFile($"appsettings.{hosting.HostingEnvironment.EnvironmentName}.json"); })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // For Api Gateway Service
                    // webBuilder.ConfigureAppConfiguration((hosting, config) => { config.RegisterMultipleOcelotConfig("GatewayConfiguration", hosting.HostingEnvironment); });
                    webBuilder.UseStartup<Startup>();
                });
    }
}