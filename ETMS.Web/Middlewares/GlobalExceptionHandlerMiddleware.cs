using System.Net;
using System.Text.Json;
using ETMS.Domain.Common;
using ETMS.Service.Exceptions;
using static ETMS.Domain.Enums.Enums;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception regardless of its type
            _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = new ApiResponse<object>();

        switch (exception)
        {
            // Your custom exception type
            case ResponseException ex:
                response = new ApiResponse<object>(ex.Message, new List<string> { ex.Message });
                context.Response.StatusCode = (int)GetHttpStatusCode(ex.Code);
                break;

            // You can add cases for other specific, common exceptions
            case KeyNotFoundException ex:
                response = new ApiResponse<object>("The specified key was not found.", new List<string> { ex.Message });
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            // The default case for any unhandled exceptions
            default:
                response = new ApiResponse<object>("An unexpected internal server error has occurred.",
                    // Only include detailed error messages in Development environment for security
                    _env.IsDevelopment() ? new List<string> { exception.Message } : null);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        await context.Response.WriteAsJsonAsync(response);
    }

    private static HttpStatusCode GetHttpStatusCode(EResponse code)
    {
        return code switch
        {
            EResponse.Accepted => HttpStatusCode.Accepted,
            EResponse.BadRequest => HttpStatusCode.BadRequest,
            EResponse.Unauthorized => HttpStatusCode.Unauthorized,
            EResponse.Forbidden => HttpStatusCode.Forbidden,
            EResponse.NotFound => HttpStatusCode.NotFound,
            EResponse.InternalServerError => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError, // Default case
        };
    }
}