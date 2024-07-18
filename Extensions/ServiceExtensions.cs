using System.Text;
using clerk.server.Configurations;
using clerk.server.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace clerk.server.Extensions;

public static class ServiceExtensions
{
    public static void ConfigurePostgresDB(this IServiceCollection services)
    {
        services.AddDbContext<RepositoryContext>(options
        => options.UseNpgsql(AppSettings.Connections.PostgresLocal));
    }

    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = AppSettings.JwtOptions.Issuer,
                ValidAudience = AppSettings.JwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(AppSettings.JwtOptions.Secret)
                    )
            };
        });

    }


}