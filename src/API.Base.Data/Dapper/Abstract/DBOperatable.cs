using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace API.Base.Data.Dapper.Abstract
{
    public interface IDBOperatable
    {
        Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<int> ExecuteAsnyc(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }

    public class DBOperatable : IDBOperatable
    {
        public DBOperatable(IDBConnection connection)
        {
            _connection = connection;
        }

        private IDBConnection _connection { get; set; }

        public Task<TResult> QueryFirstOrDefaultAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            Using(() => _connection.GetConnection(), conn => conn.QueryFirstOrDefaultAsync<TResult>(sql, param, transaction, commandTimeout, commandType));

        public Task<int> ExecuteAsnyc(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            Using(() => _connection.GetConnection(), conn => conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType));

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            Using(() => _connection.GetConnection(), conn => conn.QueryAsync<TResult>(sql, param, transaction, commandTimeout, commandType));

        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            Using(() => _connection.GetConnection(), conn => conn.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType));

        public Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
            Using(() => _connection.GetConnection(), conn => conn.ExecuteScalarAsync<TResult>(sql, param, transaction, commandTimeout, commandType));

        private static async Task<TResult> Using<TDisposable, TResult>(Func<TDisposable> factory, Func<TDisposable, Task<TResult>> map)
            where TDisposable : IDisposable
        {
            using (var disposable = factory.Invoke())
                return await map.Invoke(disposable).ConfigureAwait(false);
        }
    }
}