using System;
using System.Threading.Tasks;
using API.Base.Api.Extensions;
using API.Base.Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace API.Base.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly ILogger _logger = Log.ForContext<ExceptionMiddleware>();
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                context.Response.Clear();

                var error = exception.ToApiResponse();

                if (env.IsProduction())
                    error.Result.StackTrace = null;

                var result = new BadRequestObjectResult(error);
                await context.WriteResultAsync(result);
            }
            catch (Exception exception)
            {
                context.Response.Clear();

                var error = exception.ToApiResponse();
                _logger.Error(exception, error.ToJson());

                if (env.IsProduction())
                    return;

                var result = new BadRequestObjectResult(error);
                await context.WriteResultAsync(result);
            }
        }
    }
}