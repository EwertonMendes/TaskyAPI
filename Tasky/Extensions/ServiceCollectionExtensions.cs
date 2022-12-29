using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tasky.Data;

namespace Tasky.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskyAPI", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer"),
                ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:Key")))
            };
        });

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_NAME");
        var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? builder.Configuration.GetValue<string>("EnvironmentVariables:DB_SA_PASSWORD");
        var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;MultipleActiveResultSets=true";

        builder.Services.AddDbContext<TaskyContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
