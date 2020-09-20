using API.Base.Service;
using API.Base.Service.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var mapper = ServiceStartup.MapperInitialize();
            MapperExtensions.SetMapper(mapper);
            services.AddSingleton(mapper);
            return services;
        }
    }
}