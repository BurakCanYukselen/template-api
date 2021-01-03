using API.Base.Data;
using AutoMapper;

namespace API.Base.Service
{
    public static class ServiceStartup
    {
        public static IMapper MapperInitialize()
        {
            DataStartUp.InitializeFluentMapping();

            var configuration = new MapperConfiguration(config => { });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}