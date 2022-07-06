using Microsoft.AspNetCore.Http;
using Simple.Common.DependencyInjection;

namespace SimpleApp.Common;

public interface IApiResult
{
    int Code { get; set; }
    string? Msg { get; set; }
}

public interface IApiResult<TData> : IApiResult
{
    TData? Data { get; set; }
}

public class ApiResult : IApiResult
{
    public int Code { get; set; }
    public string? Msg { get; set; }

    public ApiResult() { }

    public static ApiResult OK()
    {
        return new ApiResult() { Code = StatusCodes.Status200OK };
    }

    public static ApiResult OK(string? msg)
    {
        return new ApiResult() { Code = StatusCodes.Status200OK, Msg = msg };
    }

    public static ApiResult BadRequest()
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest };
    }

    public static ApiResult BadRequest(string? msg)
    {
        return new ApiResult() { Code = StatusCodes.Status400BadRequest, Msg = msg };
    }

    public static ApiResult Unauthorized()
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized };
    }

    public static ApiResult Unauthorized(string? msg)
    {
        return new ApiResult() { Code = StatusCodes.Status401Unauthorized, Msg = msg };
    }

    public static ApiResult Forbidden()
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden };
    }

    public static ApiResult Forbidden(string? msg)
    {
        return new ApiResult() { Code = StatusCodes.Status403Forbidden, Msg = msg };
    }

    public static ApiResult NotFound()
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound };
    }

    public static ApiResult NotFound(string? msg)
    {
        return new ApiResult() { Code = StatusCodes.Status404NotFound, Msg = msg };
    }
}

public class ApiResult<TData> : ApiResult, IApiResult<TData>
{
    public TData? Data { get; set; }

    public ApiResult() { }

    public static ApiResult<TData> OK(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status200OK, Data = data };
    }

    public static ApiResult<TData> OK(TData data, string? msg)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status200OK, Data = data, Msg = msg };
    }

    public static ApiResult<TData> BadRequest(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status400BadRequest, Data = data };
    }

    public static ApiResult<TData> BadRequest(TData data, string? msg)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status400BadRequest, Data = data, Msg = msg };
    }

    public static ApiResult<TData> Unauthorized(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status401Unauthorized, Data = data };
    }

    public static ApiResult<TData> Unauthorized(TData data, string? msg)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status401Unauthorized, Data = data, Msg = msg };
    }

    public static ApiResult<TData> Forbidden(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status403Forbidden, Data = data };
    }

    public static ApiResult<TData> Forbidden(TData data, string? msg)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status403Forbidden, Data = data, Msg = msg };
    }

    public static ApiResult<TData> NotFound(TData data)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status404NotFound, Data = data };
    }

    public static ApiResult<TData> NotFound(TData data, string? msg)
    {
        return new ApiResult<TData>() { Code = StatusCodes.Status404NotFound, Data = data, Msg = msg };
    }
}
