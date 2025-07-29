using System.Collections;
using System.Net;
using ETMS.Domain.Common;
using ETMS.Domain.Common.Interfaces;
using ETMS.Service.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class HandleRequestResponse : ActionFilterAttribute
{
    public ETypeRequestResponse TypeResponse { get; set; }

    /// <summary>
    /// After a request 
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            // Already returning Response<T>, no need to modify
            if (context.Result is ObjectResult { Value: IResponse })
            {
                base.OnActionExecuted(context);
                return;
            }

            // Convert non-Response results to Response<T>
            if (context.Result is OkObjectResult okResult)
            {
                var responseType = typeof(Response<>).MakeGenericType(okResult.Value?.GetType() ?? typeof(object));
                var response = Activator.CreateInstance(responseType);
                
                responseType.GetProperty("Data")?.SetValue(response, okResult.Value);
                responseType.GetProperty("Succeeded")?.SetValue(response, true);
                responseType.GetProperty("Message")?.SetValue(response, "Request successful");
                responseType.GetProperty("Errors")?.SetValue(response, Array.Empty<string>());
                responseType.GetProperty("StatusCode")?.SetValue(response, HttpStatusCode.OK);

                if (TypeResponse == ETypeRequestResponse.ResponseWithData && !IsValue(okResult.Value))
                {
                    responseType.GetProperty("Succeeded")?.SetValue(response, false);
                    responseType.GetProperty("StatusCode")?.SetValue(response, HttpStatusCode.NotFound);
                    responseType.GetProperty("Message")?.SetValue(response, "No data found");
                    context.Result = new NotFoundObjectResult(response);
                }
                else
                {
                    context.Result = new OkObjectResult(response);
                }
            }
            else if (context.Result is OkResult)
            {
                var response = new Response<object>
                {
                    Data = null,
                    Succeeded = true,
                    Message = "Request successful",
                    Errors = Array.Empty<string>(),
                    StatusCode = HttpStatusCode.OK
                };
                context.Result = new OkObjectResult(response);
            }
        }
        else if (context.Exception is ResponseException exceptionResponse)
        {
            context.Result = HandleResponseException(exceptionResponse);
            context.ExceptionHandled = true;
        }
        else
        {
            var response = new Response<object>
            {
                Data = null,
                Succeeded = false,
                Message = "An unexpected error occurred",
                Errors = new[] { context.Exception.Message },
                StatusCode = HttpStatusCode.InternalServerError
            };
            context.Result = new ObjectResult(response) { StatusCode = 500 };
            context.ExceptionHandled = true;
        }

        base.OnActionExecuted(context);
    }

    private static IActionResult HandleResponseException(ResponseException exceptionResponse)
    {
        var response = new Response<object>
        {
            Data = null,
            Succeeded = false,
            Message = exceptionResponse.Message,
            Errors = new[] { exceptionResponse.Message },
            StatusCode = GetHttpStatusCode(exceptionResponse.Code)
        };

        return exceptionResponse.Code switch
        {
            EResponse.Accepted => new AcceptedResult(),
            EResponse.BadRequest => new BadRequestObjectResult(response),
            EResponse.Unauthorized => new UnauthorizedObjectResult(response),
            EResponse.Forbidden => new ObjectResult(response) { StatusCode = 403 },
            EResponse.NotFound => new NotFoundObjectResult(response),
            EResponse.InternalServerError => new ObjectResult(response) { StatusCode = 500 },
            _ => new OkObjectResult(response),
        };
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
            _ => HttpStatusCode.OK,
        };
    }

    /// <summary>
    /// Check if value exists
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static bool IsValue(object value)
    {
        if (value is null) return false;
        if (value is ICollection collection && collection.Count == 0) return false;
        return true;
    }
}

