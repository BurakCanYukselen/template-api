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
    }
}