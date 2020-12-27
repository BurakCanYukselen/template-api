using API.Base.Data.Connections.Abstract;

namespace API.Base.Data.Connections
{
    public interface IExampleDbConnection : IDBConnection
    {
    }

    public class ExampleDbConnection : DBConnection, IExampleDbConnection
    {
        public ExampleDbConnection(string connectionString) : base(connectionString)
        {
        }
    }
}