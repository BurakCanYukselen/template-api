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
                var docName = typeof(ServiceCollectionExtensions).Module.Name.Replace(".Api.dll", string.Empty);
                foreach (var version in versions)
                {
                    config.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{docName} {version}");
                }
            });

            return app;
        }
    }
}