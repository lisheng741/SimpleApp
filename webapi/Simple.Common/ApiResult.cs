using Microsoft.AspNetCore.Http;
using Simple.Common.DependencyInjection;

namespace SimpleApp.Common;

public class ApiResult
{
    public int Code { get; set; }
    public string? Message { get; set; }

    public ApiResult() { }

    public static ApiResult Status200OK()
    {
        return new ApiResult() { Code = StatusCodes.Status200OK };
    }

    public static ApiResult Status200OK(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status200OK, Message = message };
    }

    public static ApiResult Status400BadRequest()
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest };
    }

    public static ApiResult Status400BadRequest(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest, Message = message };
    }

    public static ApiResult Status401Unauthorized()
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized };
    }

    public static ApiResult Status401Unauthorized(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized, Message = message };
    }

    public static ApiResult Status403Forbidden()
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden };
    }

    public static ApiResult Status403Forbidden(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden, Message = message };
    }

    public static ApiResult Status404NotFound()
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound };
    }

    public static ApiResult Status404NotFound(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound, Message = message };
    }
}

public class ApiResult<TData> : ApiResult
{
    public TData? Data { get; set; }

    public ApiResult() { }

    public static ApiResult<TData> Status200OK(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status200OK, Data = data };
    }

    public static ApiResult<TData> Status200OK(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status200OK, Data = data, Message = message };
    }

    public static ApiResult<TData> Status400BadRequest(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status400BadRequest, Data = data };
    }

    public static ApiResult<TData> Status400BadRequest(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status400BadRequest, Data = data, Message = message };
    }

    public static ApiResult<TData> Status401Unauthorized(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status401Unauthorized, Data = data };
    }

    public static ApiResult<TData> Status401Unauthorized(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status401Unauthorized, Data = data, Message = message };
    }

    public static ApiResult<TData> Status403Forbidden(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status403Forbidden, Data = data };
    }

    public static ApiResult<TData> Status403Forbidden(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status403Forbidden, Data = data, Message = message };
    }

    public static ApiResult<TData> Status404NotFound(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status404NotFound, Data = data };
    }

    public static ApiResult<TData> Status404NotFound(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status404NotFound, Data = data, Message = message };
    }
}
