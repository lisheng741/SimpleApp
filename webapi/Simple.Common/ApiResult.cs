using Microsoft.AspNetCore.Http;
using Simple.Common.DependencyInjection;

namespace Simple.Common;

public partial class ApiResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public ApiResult() { }

    public ApiResult(int code) : this(code, null) { }

    public ApiResult(int code, string? message) : this(code, message, null) { }

    public ApiResult(int code, string? message, object? data)    {
        Code = code;
        Message = message;
        Data = data;
    }
}

public partial class ApiResult
{
    public static ApiResult Status200OK() => Status200OK(null);

    public static ApiResult Status200OK(string? message) => Status200OK(message, null);

    public static ApiResult Status200OK(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status200OK, Message = message, Data = data };
    }

    public static ApiResult Status400BadRequest() => Status400BadRequest(null);

    public static ApiResult Status400BadRequest(string? message) => Status400BadRequest(message, null);

    public static ApiResult Status400BadRequest(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest, Message = message, Data = data };
    }

    public static ApiResult Status401Unauthorized() => Status401Unauthorized(null);

    public static ApiResult Status401Unauthorized(string? message) => Status401Unauthorized(message, null);

    public static ApiResult Status401Unauthorized(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized, Message = message, Data = data };
    }

    public static ApiResult Status403Forbidden() => Status403Forbidden(null);

    public static ApiResult Status403Forbidden(string? message) => Status403Forbidden(message, null);

    public static ApiResult Status403Forbidden(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden, Message = message, Data = data };
    }

    public static ApiResult Status404NotFound() => Status404NotFound(null);

    public static ApiResult Status404NotFound(string? message) => Status404NotFound(message, null);

    public static ApiResult Status404NotFound(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound, Message = message, Data = data };
    }

    public static ApiResult Status500InternalServerError() => Status500InternalServerError(null);

    public static ApiResult Status500InternalServerError(string? message) => Status500InternalServerError(message, null);

    public static ApiResult Status500InternalServerError(string? message, object? data)
    {
        return new ApiResult() { Code = StatusCodes.Status500InternalServerError, Message = message, Data = data };
    }
}
