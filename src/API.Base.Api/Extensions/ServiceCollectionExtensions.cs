using System.Linq;
using System.Reflection;
using API.Base.Api.Filters.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Base.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static TSetting RegisterSettings<TSetting>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where TSetting : class
        {
            var section = configuration.GetSection(sectionName);
            var setting = section.Get<TSetting>();
            services.AddSingleton(setting);
            return setting;
        }

        public static IServiceCollection AddSwaggerDocument(this IServiceCollection services, params string[] versions)
        {
            services.AddSwaggerGen(config =>
            {
                var docName = typeof(ServiceCollectionExtensions).Module.Name.Replace(".Api.dll", string.Empty);
                foreach (var version in versions)
                {
                    config.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = $"{docName} {version}",
                        Version = version
                    });
                }

                config.OperationFilter<RemoveVersionFromParameter>();
                config.DocumentFilter<ReplaceVersionInPath>();
                config.DocInclusionPredicate((name, description) =>
                {
                    if (!description.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var version = methodInfo.DeclaringType!
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(p => p.Versions);

                    return version.Any(p => $"v{p.ToString()}" == name);
                    //return true;
                });
            });
            return services;
        }
    }
}