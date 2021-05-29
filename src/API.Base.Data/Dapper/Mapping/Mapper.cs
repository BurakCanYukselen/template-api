using Dapper.FluentMap;

namespace API.Base.Data.Dapper.Mapping
{
    public class Mapper
    {
        public static void InitializeFluentMapping()
        {
            FluentMapper.Initialize(cfg => { });
        }
    }
}