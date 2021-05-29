using System.Collections.Generic;
using System.Linq;

namespace API.Base.Api.Realtime.Infrastructure
{
    public class ConnectionMapping<TKey>
    {
        private readonly Dictionary<TKey, HashSet<string>> _connections = new Dictionary<TKey, HashSet<string>>();

        public int Count => _connections.Count;

        public void Add(TKey key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out var connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (_connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(TKey key)
        {
            if (_connections.TryGetValue(key, out var connections))
                return connections;

            return Enumerable.Empty<string>();
        }

        public void Remove(TKey key, string connectionId)
        {
            lock (_connections)
            {
                if (!_connections.TryGetValue(key, out var connections))
                    return;

                lock (_connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}