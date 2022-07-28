using Microsoft.AspNetCore.Http;
using Simple.Common.DependencyInjection;

namespace Simple.Common;

public class ApiResult
{
    public int Code { get; set; }
    public string? Message { get; set; }

    public ApiResult() { }

    public static ApiResult Status200OK() => Status200OK(null);

    public static ApiResult Status200OK(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status200OK, Message = message };
    }

    public static ApiResult Status400BadRequest() => Status400BadRequest(null);

    public static ApiResult Status400BadRequest(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest, Message = message };
    }

    public static ApiResult Status401Unauthorized() => Status401Unauthorized(null);

    public static ApiResult Status401Unauthorized(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized, Message = message };
    }

    public static ApiResult Status403Forbidden() => Status403Forbidden(null);

    public static ApiResult Status403Forbidden(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden, Message = message };
    }

    public static ApiResult Status404NotFound() => Status404NotFound(null);

    public static ApiResult Status404NotFound(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound, Message = message };
    }

    public static ApiResult Status500InternalServerError() => Status500InternalServerError(null);

    public static ApiResult Status500InternalServerError(string? message)
    {
        return new ApiResult() { Code = StatusCodes.Status500InternalServerError, Message = message };
    }
}

public class ApiResult<TData> : ApiResult
{
    public TData? Data { get; set; }

    public ApiResult() { }

    public static ApiResult<TData> Status200OK(TData data) => Status200OK(data, null);

    public static ApiResult<TData> Status200OK(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status200OK, Data = data, Message = message };
    }

    public static ApiResult<TData> Status400BadRequest(TData data) => Status400BadRequest(data, null);

    public static ApiResult<TData> Status400BadRequest(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status400BadRequest, Data = data, Message = message };
    }

    public static ApiResult<TData> Status401Unauthorized(TData data) => Status401Unauthorized(data, null);

    public static ApiResult<TData> Status401Unauthorized(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status401Unauthorized, Data = data, Message = message };
    }

    public static ApiResult<TData> Status403Forbidden(TData data) => Status403Forbidden(data, null);

    public static ApiResult<TData> Status403Forbidden(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status403Forbidden, Data = data, Message = message };
    }

    public static ApiResult<TData> Status404NotFound(TData data) => Status404NotFound(data, null);

    public static ApiResult<TData> Status404NotFound(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status404NotFound, Data = data, Message = message };
    }

    public static ApiResult<TData> Status500InternalServerError(TData data) => Status500InternalServerError(data, null);

    public static ApiResult<TData> Status500InternalServerError(TData data, string? message)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status500InternalServerError, Data = data, Message = message };
    }
}
