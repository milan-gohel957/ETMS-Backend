namespace ETMS.Domain.Enums;

public class Enums
{
    public enum EResponse
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }

    public enum ETypeRequestResponse
    {
        Default = 0,
        ResponseWithData = 1
    }
}
