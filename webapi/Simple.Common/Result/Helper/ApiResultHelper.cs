using Microsoft.AspNetCore.Http;
using Simple.Common.Result;

namespace Simple.Common.Helpers;

public static class ApiResultHelper
{
    public static ApiResult Result(int code = StatusCodes.Status200OK, string? message = ApiResultMessage.Status200OK, object? data = null, bool success = true)
    {
        return new ApiResult(code, message, data, success);
    }

    public static ApiResult Result200OK(string? message = ApiResultMessage.Status200OK, object? data = null)
    {
        return new ApiResult(StatusCodes.Status200OK, message, data, true);
    }

    public static ApiResult Result400BadRequest(string? message = ApiResultMessage.Status400BadRequest, object? data = null)
    {
        return new ApiResult(StatusCodes.Status400BadRequest, message, data, false);
    }

    public static ApiResult Result401Unauthorized(string? message = ApiResultMessage.Status401Unauthorized, object? data = null)
    {
        return new ApiResult(StatusCodes.Status401Unauthorized, message, data, false);
    }

    public static ApiResult Result403Forbidden(string? message = ApiResultMessage.Status403Forbidden, object? data = null)
    {
        return new ApiResult(StatusCodes.Status403Forbidden, message, data, false);
    }

    public static ApiResult Result404NotFound(string? message = ApiResultMessage.Status404NotFound, object? data = null)
    {
        return new ApiResult(StatusCodes.Status404NotFound, message, data, false);
    }

    public static ApiResult Result409Conflict(string? message = ApiResultMessage.Status409Conflict, object? data = null)
    {
        return new ApiResult(StatusCodes.Status409Conflict, message, data, false);
    }

    public static ApiResult Result500InternalServerError(string? message = ApiResultMessage.Status500InternalServerError, object? data = null)
    {
        return new ApiResult(StatusCodes.Status500InternalServerError, message, data, false);
    }

    public static string GetMessage(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status200OK => ApiResultMessage.Status200OK,
            StatusCodes.Status400BadRequest => ApiResultMessage.Status400BadRequest,
            StatusCodes.Status401Unauthorized => ApiResultMessage.Status401Unauthorized,
            StatusCodes.Status403Forbidden => ApiResultMessage.Status403Forbidden,
            StatusCodes.Status404NotFound => ApiResultMessage.Status404NotFound,
            StatusCodes.Status409Conflict => ApiResultMessage.Status409Conflict,
            StatusCodes.Status500InternalServerError => ApiResultMessage.Status500InternalServerError,
            _ => ""
        };
    }

    public static bool GetSuccess(int statusCode)
    {
        if (statusCode >= 200 && statusCode <= 299)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
