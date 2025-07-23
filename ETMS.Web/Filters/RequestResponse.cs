
using ETMS.Service.Exceptions;
using static ETMS.Domain.Enums.Enums;

namespace ETMS.Web.Filters;


internal static class RequestResponse
{
    public static void UnSuccess(string message)
    {
        ExceptionResponse(EResponse.BadRequest, message);
    }
    public static void UnSuccess(bool condition, string message = null!)
    {
        if (condition)
            UnSuccess(message);
    }

    public static void NotAuthorized(string message = "")
    {
        ExceptionResponse(EResponse.Unauthorized, message);
    }

    public static void CheckingAuthorize(bool condition, string message = "")
    {
        if (condition)
            NotAuthorized(message);
    }

    private static void ExceptionResponse(EResponse response, string message)
    {
        throw new ResponseException(response, message);
    }
}