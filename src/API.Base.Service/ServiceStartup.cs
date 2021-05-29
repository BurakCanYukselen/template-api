using API.Base.Data;
using AutoMapper;

namespace API.Base.Service
{
    public static class ServiceStartup
    {
        public static IMapper MapperInitialize()
        {
            DataStartUp.Setup();

            var configuration = new MapperConfiguration(config => { });
            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}