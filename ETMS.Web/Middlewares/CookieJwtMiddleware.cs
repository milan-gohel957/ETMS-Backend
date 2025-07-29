namespace ETMS.Web.Middlewares;

public class CookieJwtMiddleware
{
    private readonly RequestDelegate _next;

    public CookieJwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Read the token from your secure httpOnly cookie (e.g., "AccessToken")
        var token = context.Request.Cookies["AccessToken"];
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers["Authorization"] = "Bearer " + token;
        }

        await _next(context);
    }
}
