using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Base.Api.Filters.Attributes.BaseAttributes
{
    public abstract class BaseValidationOwnershipAttribute : ActionFilterAttribute
    {
        protected Guid? GetFromRouteValue(ActionExecutingContext context, string key)
        {
            if (context.ActionArguments.TryGetValue(key, out var valueObject))
            {
                if (Guid.TryParse((string) valueObject, out var value))
                    return value;
            }

            return null;
        }

        protected TService GetService<TService>(ActionExecutingContext context)
        {
            return (TService) context.HttpContext.RequestServices.GetService(typeof(TService));
        }
    }
}