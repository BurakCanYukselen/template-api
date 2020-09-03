using System;
using System.Threading.Tasks;
using API.Base.Api.Extensions;
using API.Base.Core.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace API.Base.Api.Middlewares
{
    public class ExceptionMiddleware
    {
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
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var error = exception.ToApiResponse();

                if (env.IsDevelopment())
                    error.Result.StackTrace = null;
                
                await context.WriteResultAsync(new ObjectResult(error));
                return;
            }
            catch (Exception exception)
            {
                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var error = exception.ToApiResponse();

                if (env.IsProduction())
                    return;

                await context.WriteResultAsync(new ObjectResult(error));
            }
        }
    }
}