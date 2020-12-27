using System;
using System.Threading.Tasks;
using API.Base.Core.Extensions;
using API.Base.Realtime.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace API.Base.Realtime.Infrastructure
{
    public abstract class AbstractHub<TConnectionKey> : Hub
    {
        private readonly ConnectionMapping<TConnectionKey> _connections;

        public AbstractHub(ConnectionMapping<TConnectionKey> connections)
        {
            _connections = connections;
        }

        public override Task OnConnectedAsync()
        {
            var userid = Context.GetContextHeaderValue("userid");
            var connectionKey = userid.ConvertTo<TConnectionKey>();
            _connections.Add(connectionKey, Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userid = Context.GetContextHeaderValue("userid");
            var connectionKey = userid.ConvertTo<TConnectionKey>();
            _connections.Remove(connectionKey, Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}