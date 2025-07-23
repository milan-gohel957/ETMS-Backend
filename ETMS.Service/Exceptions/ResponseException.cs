using static ETMS.Domain.Enums.Enums;

namespace ETMS.Service.Exceptions;

public class ResponseException(EResponse code, string message) : Exception(message)
{
    public EResponse Code { get; set; } = code;
}