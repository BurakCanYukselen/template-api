using System.Linq;
using API.Base.Api.Filters.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static TSetting RegisterSettings<TSetting>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where TSetting : class
        {
            var section = configuration.GetSection(sectionName);
            var setting = section.Get<TSetting>();
            services.AddSingleton(setting);
            return setting;
        }
    }
}