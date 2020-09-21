using API.Base.Api.Extensions.ApplicationBuilderExtensions;
using API.Base.Api.Extensions.ServiceCollectionExtensions;
using API.Base.Api.Middlewares;
using API.Base.Core.Behaviors;
using API.Base.Core.Settings;
using API.Base.Data;
using API.Base.Data.Connections;
using API.Base.External;
using API.Base.Realtime.Hubs;
using API.Base.Service;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

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
            services.AddSignalR();
            services.AddMediatR(new [] {typeof(DataStartUp).Assembly, typeof(ServiceStartup).Assembly});
            services.AddValidatorsFromAssemblies(new[] {typeof(DataStartUp).Assembly, typeof(ServiceStartup).Assembly});
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // For Api Gateway Service
            // services.AddOcelot();

            services.RegisterApiVersioning(appSetting.DefaultApiVersion.MajorVersion, appSetting.DefaultApiVersion.MinorVersion);
            services.RegisterJwtBearerAuthentication("AuthUser", appSetting.ApplicationSecret);
            services.RegisterSwagger(appSetting.Swagger.AvailableVersions);
            services.RegisterAutoMapper();

            services.RegisterExternalService<IExampleExternalService, ExampleExternalService, ExampleExternalServiceHttpClient>(appSetting.ExternalServices.ExampleExternalService);
            services.RegisterDbConnection<IExampleDbConnection, ExampleDbConnection>(appSetting.ConnectionStrings.ExampleDbConnection);
            services.RegisterSignalRManager<ExampleHubManager, ExampleConnection>();
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppSettings settings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ExampleHub>("/hub/example");
            });
            // For Api Gateway Service
            // await app.UseOcelot();
            app.RunSwagger(settings.Swagger.AvailableVersions);
        }
    }
}