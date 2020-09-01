using System.Data.SqlClient;

namespace API.Base.Data.Connections.Abstract
{
    public interface IDbConnection
    {
        SqlConnection GetConnection();
    }
    
    public abstract class DbConnection: IDbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}