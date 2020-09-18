using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Base.Api.Extensions.ServiceCollectionExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterJwtBearerAuthentication(this IServiceCollection services, string authenticationScheme, string applicationSecret)
        {
            services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(authenticationScheme, config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(applicationSecret)),
                    };
                });
            services.AddAuthorization();
            return services;
        }
    }
}