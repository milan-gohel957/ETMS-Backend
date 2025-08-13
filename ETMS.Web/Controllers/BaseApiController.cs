using ETMS.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace ETMS.Web.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    //  existing generic method for responses with data
    protected IActionResult Success<T>(T? data, string? message = null)
    {
        return Ok(new ApiResponse<T?>(data, message));
    }

    protected IActionResult Success(string? message = "Request was successful.")
    {
        // It calls the generic method with 'object' as the type
        return Success<object>(null, message);
    }

    //  existing Failure method...
    protected IActionResult Failure(string message, List<string>? errors = null, int statusCode = 400)
    {
        return StatusCode(statusCode, new ApiResponse<object>(message, errors));
    }
}