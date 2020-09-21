using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterExternalService<TService, TImplementation, THttpClient>(this IServiceCollection services, string externalServiceUrl)
            where TService : class
            where TImplementation : class, TService
            where THttpClient : HttpClient
        {
            var httpClientConstractor = typeof(THttpClient).GetConstructor(new[] {typeof(string)});
            var httpClientImplementation = (THttpClient) httpClientConstractor!.Invoke(new object[] {externalServiceUrl});
            services.AddSingleton(httpClientImplementation);
            services.AddScoped<TService, TImplementation>();
            return services;
        }
    }
}