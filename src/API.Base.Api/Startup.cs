using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Base.Api.Extensions;
using API.Base.Api.Filters.Swagger;
using API.Base.Core.Infrastructure;
using API.Base.Core.Infrastructure.Settings;
using API.Base.Data;
using API.Base.Data.Connections;
using API.Base.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API.Base.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = services.RegisterSettings<AppSettings>(Configuration, "AppSettings");

            services.AddControllers();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(appSetting.DefaultApiVersion.MajorVersion, appSetting.DefaultApiVersion.MinorVersion);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwaggerDocument(appSetting.Swagger.AvailableVersions);
            services.AddMediatR(new Assembly[] {typeof(DataStartUp).Assembly, typeof(ServiceStartup).Assembly});

            services.AddSingleton<IExampleDbConnection>(new ExampleDbConnection(appSetting.ConnectionStrings.ExampleDbConnection));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerWithVersion(settings.Swagger.AvailableVersions);
        }
    }
}