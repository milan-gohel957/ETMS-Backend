using System.Net;
using ETMS.Domain.Common.Interfaces;

namespace ETMS.Domain.Common;

public class ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    public ApiResponse()
    {
        // Initialize properties to sensible defaults
        Succeeded = true; // Or false, depending on your default assumption
        Message = string.Empty;
        Data = default;
        Errors = null;
    }
    public ApiResponse(T? data, string? message = null)
    {
        Succeeded = true;
        Message = message ?? "Request successful.";
        Data = data;
        Errors = null;
    }

    public ApiResponse(string message, List<string>? errors = null)
    {
        Succeeded = false;
        Message = message;
        Data = default;
        Errors = errors ?? new List<string>();
    }
}
