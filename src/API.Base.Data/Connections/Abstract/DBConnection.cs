using System.Data.SqlClient;

namespace API.Base.Data.Connections.Abstract
{
    public interface IDBConnection
    {
        IDBOperatable DB { get; }
        SqlConnection GetConnection();
    }
    
    public abstract class DBConnection: IDBConnection
    {
        private readonly string _connectionString;
        public IDBOperatable DB { get; }

        public DBConnection(string connectionString)
        {
            _connectionString = connectionString;
            DB = new DBOperatable(this);
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}