using System.Net;
using ETMS.Domain.Common.Interfaces;

namespace ETMS.Domain.Common;

public class Response<T> : IResponse
{
    public T? Data { get; set; }
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string[]? Errors { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}