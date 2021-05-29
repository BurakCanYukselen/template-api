using API.Base.Data.Dapper.Mapping;

namespace API.Base.Data
{
    public class DataStartUp
    {
        public static void Setup()
        {
            Mapper.InitializeFluentMapping();
        }
    }
}