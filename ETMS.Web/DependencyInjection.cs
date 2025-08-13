
using System.Reflection;
using System.Text;
using ETMS.Service.DTOs;
using ETMS.Repository.Context;
using ETMS.Repository.Repositories;
using ETMS.Repository.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using ETMS.Domain.Common;

namespace ETMS.Web;

public static class DependencyInjection
{

    public static void RegisterServices(IServiceCollection services, string connectionString, IConfiguration config)
    {

        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                );
            });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.Configure<JwtSettings>(config.GetSection("Jwt"));

        var allReferencedTypes = Assembly
            .GetAssembly(typeof(DependencyInjection))!
            .GetReferencedAssemblies()
            .Select(Assembly.Load)
            .SelectMany(a => a.GetTypes())
            .Where(t =>
                t.Namespace != null
                && (
                    t.Namespace.StartsWith("ETMS.Service")
                )
            )
            .ToList();
        var interfaces = allReferencedTypes.Where(t => t.IsInterface);

        foreach (var serviceInterface in interfaces)
        {
            var implementation = allReferencedTypes.FirstOrDefault(c =>
                c.IsClass && !c.IsAbstract && serviceInterface.Name.Substring(1) == c.Name
            );

            if (implementation != null)
            {
                services.AddScoped(serviceInterface, implementation);
            }
        }
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = config.GetSection("Jwt").Get<JwtSettings>();
                if (jwtSettings == null) return;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });
    }
}