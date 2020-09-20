using AutoMapper;

namespace API.Base.Service
{
    public static class ServiceStartup
    {
        public static IMapper MapperInitialize()
        {
            var configuration = new MapperConfiguration(config =>
            {
            });

            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}