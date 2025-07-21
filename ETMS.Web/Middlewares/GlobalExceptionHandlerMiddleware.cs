using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Process request
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Default response details
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";

        // Optional: You can customize based on exception type
        // if (exception is NotFoundException) { ... }

        // Optionally: Log the exception, e.g. using ILogger

        var response = new
        {
            error = message,
            // You can add more fields if needed: errorCode, stackTrace, etc
            details = exception.Message // For development only; remove or limit for production!
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }
}