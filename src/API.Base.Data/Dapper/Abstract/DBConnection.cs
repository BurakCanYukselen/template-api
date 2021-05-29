using System.Data.SqlClient;

namespace API.Base.Data.Dapper.Abstract
{
    public interface IDBConnection
    {
        IDBOperatable DB { get; }
        SqlConnection GetConnection();
    }

    public abstract class DBConnection : IDBConnection
    {
        private readonly string _connectionString;

        public DBConnection(string connectionString)
        {
            _connectionString = connectionString;
            DB = new DBOperatable(this);
        }

        public IDBOperatable DB { get; }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}