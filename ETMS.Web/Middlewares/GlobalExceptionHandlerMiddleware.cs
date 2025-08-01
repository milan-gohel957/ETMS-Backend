using ETMS.Domain.Common;
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
        var response = new Response<object>
        {
            Data = null,
            Succeeded = false,
            Message = "An unexpected error occurred.",
            Errors = new[] { exception.Message },
            StatusCode = HttpStatusCode.InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }

}