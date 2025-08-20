using Microsoft.EntityFrameworkCore;
using ETMS.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using ETMS.Web.Providers;
using ETMS.Service.Mappings.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ETMS.Domain.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(e => e.Value.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        Console.WriteLine($"Validation failed: {string.Join(", ", context.ModelState)}");
        var response = new ApiResponse<object>("Validation Failed", errors);
        return new BadRequestObjectResult(response);
    };
});

// Register services and dependencies
DependencyInjection.RegisterServices(
    builder.Services,
    builder.Configuration.GetConnectionString("ETMS") ?? "",
    builder.Configuration
    );


builder.Services.AddAutoMapper(
    typeof(IAutoMapperProfile).Assembly
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:4200", "http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireRole("Admin"));
});

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
// app.UseMiddleware<CookieJwtMiddleware>();

app.UseCors("AllowSpecificOrigin");
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