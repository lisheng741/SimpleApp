using Microsoft.AspNetCore.Http;

namespace SimpleApp.Common;

public interface IApiResponse
{
    int Code { get; set; }
    string? Msg { get; set; }
}

public interface IApiResponse<TData> : IApiResponse
{
    TData? Data { get; set; }
}

public class ApiResponse : IApiResponse
{
    public int Code { get; set; }
    public string? Msg { get; set; }

    public ApiResponse() { }

    public static ApiResponse OK()
    {
        return new ApiResponse() { Code = StatusCodes.Status200OK };
    }

    public static ApiResponse OK(string? msg)
    {
        return new ApiResponse() { Code = StatusCodes.Status200OK, Msg = msg };
    }

    public static ApiResponse Fail()
    {
        return new ApiResponse() { Code = StatusCodes.Status400BadRequest };
    }

    public static ApiResponse Fail(string? msg)
    {
        return new ApiResponse() { Code = StatusCodes.Status400BadRequest, Msg = msg };
    }
}

public class ApiResponse<TData> : ApiResponse, IApiResponse<TData>
{
    public TData? Data { get; set; }

    public ApiResponse() { }

    public static ApiResponse<TData> OK(TData data)
    {
        return new ApiResponse<TData>() { Code = StatusCodes.Status200OK, Data = data };
    }

    public static ApiResponse<TData> OK(TData data, string? msg)
    {
        return new ApiResponse<TData>() { Code = StatusCodes.Status200OK, Data = data, Msg = msg };
    }

    public static ApiResponse<TData> Fail(TData data)
    {
        return new ApiResponse<TData>() { Code = StatusCodes.Status400BadRequest, Data = data };
    }

    public static ApiResponse<TData> Fail(TData data, string? msg)
    {
        return new ApiResponse<TData>() { Code = StatusCodes.Status400BadRequest, Data = data, Msg = msg };
    }
}
