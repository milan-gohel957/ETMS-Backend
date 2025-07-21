using static ETMS.Domain.Enums.Enums;

namespace ETMS.Domain.Exceptions;

public class ResponseException(EResponse code, string message) : Exception(message)
{
    public EResponse Code { get; set; } = code;
}