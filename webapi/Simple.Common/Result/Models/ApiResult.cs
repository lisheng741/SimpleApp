using Microsoft.AspNetCore.Http;
using Simple.Common.Result;

namespace Simple.Common;

public class ApiResult
{
    public int Code { get; set; }

    public string? Message { get; set; }

    public object? Data { get; set; }

    public bool Success { get; set; }

    public ApiResult(int code = StatusCodes.Status200OK, string? message = ApiResultMessage.Status200OK, object? data = null, bool success = true)
    {
        Code = code;
        Message = message;
        Data = data;
        Success = success;
    }
}
