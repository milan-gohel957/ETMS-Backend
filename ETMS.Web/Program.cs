using Microsoft.EntityFrameworkCore;
using ETMS.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using ETMS.Web.Providers;
using ETMS.Service.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register services and dependencies
DependencyInjection.RegisterServices(
    builder.Services,
    builder.Configuration.GetConnectionString("ETMS") ?? "",
    builder.Configuration);

var assemblies = AppDomain.CurrentDomain.GetAssemblies()
    .Where(a => a.FullName != null && a.FullName.StartsWith("ETMS.Domain.Entities"));
builder.Services.AddAutoMapper(
    typeof(ProjectProfile),
    typeof(BoardProfile)
);

builder.Services.AddEndpointsApiExplorer();

// builder.Services.AddAuthorization(async options =>
// {
//     using (var scope = builder.Services.BuildServiceProvider().CreateScope())
//     {
//         var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
//         var dynamicPermissions = await permissionService.GetAllPermissionsAsync(); // AVOID .Result in production code - use .Wait() or make this async if possible. This is a simplification for illustration.

//         foreach (var permName in dynamicPermissions)
//         {
//             var httpContext = _httpContextAccessor.HttpContext;
//             var projectId = httpContext.Request.RouteValues["projectId"]
//                 ?? httpContext.Request.Query["projectId"];

//             projectId = projectId != null ? int.Parse(projectId.ToString()) : 0;
//             options.AddPolicy(permName, policy =>
//             {
//                 policy.RequireAuthenticatedUser();
//                 policy.AddRequirements(new PermissionRequirement(permName));
//             });
//         }

//     }
// });
builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicPolicyProvider>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project Management API", Version = "v1" });

    // Add JWT Bearer token support
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddControllers();

var app = builder.Build();

// Middlewares
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// This maps controllers and implicitly handles routing for minimal hosting
app.MapControllers();

app.Run();