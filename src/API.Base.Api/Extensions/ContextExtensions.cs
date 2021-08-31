using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace API.Base.Api.Extensions
{
    public static class ContextExtensions
    {
        public static Task WriteResultAsync(this HttpContext context, ObjectResult result)
        {
            var executor = context.RequestServices.GetRequiredService<IActionResultExecutor<ObjectResult>>();
            var routeData = context.GetRouteData() ?? new RouteData();
            var actionContext = new ActionContext(context, routeData, new ActionDescriptor());

            return executor.ExecuteAsync(actionContext, result);
        }
        
        public static TValue GetFromActionArguments<TValue>(this ActionExecutingContext context, params string[] keys)
        {
            var value = context.ActionArguments.FirstOrDefault(p => keys.Contains(p.Key, StringComparer.InvariantCultureIgnoreCase)).Value;
            if (value == null)
                return default;
            if (typeof(TValue) == typeof(Guid))
            {
                var guid = new Guid(value.ToString());
                return (TValue) Convert.ChangeType(guid, typeof(TValue));
            }

            return (TValue) value;
        }

        public static TService GetService<TService>(this ActionExecutingContext context)
        {
            var service = context.HttpContext.GetService<TService>();
            return service;
        }

        public static TService GetService<TService>(this HttpContext context)
        {
            var service = context.RequestServices.GetService<TService>();
            return service;
        }
    }
}
