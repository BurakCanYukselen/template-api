using API.Base.Data.Dapper.Abstract;

namespace API.Base.Data.Dapper
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