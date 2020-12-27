using Dapper.FluentMap;

namespace API.Base.Data
{
    public class DataStartUp
    {
        public static void InitializeFluentMapping()
        {
            FluentMapper.Initialize(cfg => { });
        }
    }
}