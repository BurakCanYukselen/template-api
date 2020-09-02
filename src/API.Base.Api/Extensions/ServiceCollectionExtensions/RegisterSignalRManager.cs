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
        public static IServiceCollection RegisterSignalRManager<THubManager, TConnection>(this IServiceCollection services)
            where THubManager : class
            where TConnection : class
        {
            services.AddSingleton<THubManager>();
            services.AddSingleton<TConnection>();
            return services;
        }
    }
}