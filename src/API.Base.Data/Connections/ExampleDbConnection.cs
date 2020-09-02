using API.Base.Data.Connections.Abstract;

namespace API.Base.Data.Connections
{
    public interface IExampleDbConnection: IDbConnection
    {
    }

    public class ExampleDbConnection: DbConnection, IExampleDbConnection
    {
        public ExampleDbConnection(string connectionString) : base(connectionString)
        {
        }
    }
}