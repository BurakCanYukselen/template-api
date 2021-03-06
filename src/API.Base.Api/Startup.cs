using API.Base.Api.Extensions.ApplicationBuilderExtensions;
using API.Base.Api.Extensions.ServiceCollectionExtensions;
using API.Base.Api.Middlewares;
using API.Base.Api.Realtime.Hubs;
using API.Base.Core.Behaviors;
using API.Base.Core.Settings;
using API.Base.Data;
using API.Base.Data.Dapper;
using API.Base.Data.EF.Context;
using API.Base.External;
using API.Base.Service;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API.Base.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSetting = services.RegisterSettings<AppSettings>(Configuration, "AppSettings");

            services.AddControllers();
            services.AddSignalR();
            services.AddMediatR(new[] {typeof(DataStartUp).Assembly, typeof(ServiceStartup).Assembly});
            services.AddValidatorsFromAssemblies(new[] {typeof(DataStartUp).Assembly, typeof(ServiceStartup).Assembly});
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // For Api Gateway Service
            // services.AddOcelot();

            services.RegisterApiVersioning(appSetting.DefaultApiVersion.MajorVersion, appSetting.DefaultApiVersion.MinorVersion);
            services.RegisterJwtBearerAuthentication("AuthUser", appSetting.ApplicationSecret);
            services.RegisterAutoMapper();

            if (!Environment.IsProduction())
                services.RegisterSwagger(appSetting.Swagger.AvailableVersions);

            services.AddDbContext<APIBaseContext>(options =>
            {
                options.UseNpgsql(appSetting.ConnectionStrings.ExampleDbConnection, configure => configure.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                options.UseSnakeCaseNamingConvention();
            });
            services.RegisterExternalService<IExampleExternalService, ExampleExternalService, ExampleExternalServiceHttpClient>(appSetting.ExternalServices.ExampleExternalService);
            services.RegisterDbConnection<IExampleDbConnection, ExampleDbConnection>(appSetting.ConnectionStrings.ExampleDbConnection);
            services.RegisterSignalRManager<ExampleHubManager, ExampleConnection>();
        }

        public async void Configure(IApplicationBuilder app, AppSettings settings)
        {
            if (Environment.IsDevelopment())
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
            if (!Environment.IsProduction())
                app.RunSwagger(settings.Swagger.AvailableVersions);
        }
    }
}