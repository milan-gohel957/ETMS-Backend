using System.Collections;
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
    /// After a resquest 
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            object valueResult = null;
    
            if (context.Result is OkObjectResult objectResult)
                valueResult = objectResult.Value;

            if (TypeResponse == ETypeRequestResponse.ResponseWithData)
            {
                if (!IsValue(valueResult))
                    context.Result = new NotFoundResult();
            }
        }
        else if (context.Exception is ResponseException exceptionResponse)
        {
            context.Result = Handled(exceptionResponse);
            context.ExceptionHandled = true;
        }
        else
        {
            context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            context.ExceptionHandled = true;
        }

        base.OnActionExecuted(context);
    }

    private static IActionResult Handled(ResponseException exceptionResponse)
    {
        string message = string.IsNullOrWhiteSpace(exceptionResponse.Message) ? string.Empty : exceptionResponse.Message;

        return exceptionResponse.Code switch
        {
            EResponse.Accepted => new AcceptedResult(),
            EResponse.BadRequest => new BadRequestObjectResult(message),
            EResponse.Unauthorized => new UnauthorizedObjectResult(message),
            EResponse.Forbidden => new ForbidResult(),
            EResponse.NotFound => new NotFoundObjectResult(message),
            EResponse.InternalServerError => new StatusCodeResult(StatusCodes.Status500InternalServerError),
            _ => new OkResult(),
        };

    }


    /// <summary>
    /// Check if is value 
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