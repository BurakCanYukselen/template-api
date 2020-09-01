using Microsoft.AspNetCore.Builder;

namespace API.Base.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerWithVersion(this IApplicationBuilder app, params string[] versions)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                foreach (var version in versions)
                {
                    config.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Garsoniyer.API.Base {version}");
                }
            });

            return app;
        }
    }
}