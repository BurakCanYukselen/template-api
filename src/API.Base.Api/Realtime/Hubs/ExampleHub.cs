using System;
using API.Base.Api.Realtime.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace API.Base.Api.Realtime.Hubs
{
    using THub = ExampleHub;
    using TMessageModel = String;
    using TConnection = ExampleConnection;
    using TConncetionKey = String;

    public class ExampleHubManager : AbstractHubManager<THub, TMessageModel, TConnection, TConncetionKey>
    {
        public ExampleHubManager(IHubContext<ExampleHub> hubContext, ExampleConnection connections) : base(hubContext, connections)
        {
        }
    }

    public class ExampleHub : AbstractHub<TConncetionKey>
    {
        public ExampleHub(ExampleConnection connections) : base(connections)
        {
        }
    }

    public class ExampleHubModel : AbstractHubMessage<TMessageModel, TConncetionKey>
    {
    }

    public class ExampleConnection : ConnectionMapping<TConncetionKey>
    {
    }
}