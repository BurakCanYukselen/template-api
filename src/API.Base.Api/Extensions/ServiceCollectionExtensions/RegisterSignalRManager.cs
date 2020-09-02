using Microsoft.Extensions.DependencyInjection;

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