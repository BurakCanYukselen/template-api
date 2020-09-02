using Microsoft.AspNetCore.SignalR;

namespace API.Base.Realtime.Extensions
{
    public static class ContextExtensions
    {
        public static string GetContextHeaderValue(this HubCallerContext context, string key)
        {
            var request = context.GetHttpContext().Request;
            request.Headers.TryGetValue(key, out var value);
            return value;
        }
    }
}