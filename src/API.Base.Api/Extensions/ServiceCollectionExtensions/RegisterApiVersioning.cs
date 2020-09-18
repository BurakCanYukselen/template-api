using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApiVersioning(this IServiceCollection services, int majorVersion, int minorVersion)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });
            return services;
        }
    }
}