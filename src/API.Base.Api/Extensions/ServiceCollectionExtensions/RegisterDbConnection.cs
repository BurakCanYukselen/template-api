using System;
using API.Base.Data.Connections.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbConnection<TService, TImplementation>(this IServiceCollection services, string connectionString)
            where TService : class, IDbConnection
            where TImplementation : class, TService
        {
            var constractor = typeof(TImplementation).GetConstructor(new Type[] {typeof(string)});
            var implementation = (TImplementation) constractor!.Invoke(new object[] {connectionString});
            services.AddSingleton<TService>(implementation);

            return services;
        }
    }
}