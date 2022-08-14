using Microsoft.AspNetCore.Http;

namespace Simple.Common;

public partial class AppResult
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public bool Success { get; set; }

    public AppResult(int code = StatusCodes.Status200OK, string? message = AppResultMessage.Status200OK, object? data = null, bool success = true)
    {
        Code = code;
        Message = message;
        Data = data;
        Success = success;
    }
}

public partial class AppResult
{
    public static AppResult Status200OK(string? message = AppResultMessage.Status200OK, object? data = null)
    {
        return new AppResult(StatusCodes.Status200OK, message, data, true);
    }

    public static AppResult Status400BadRequest(string? message = AppResultMessage.Status400BadRequest, object? data = null)
    {
        return new AppResult(StatusCodes.Status400BadRequest, message, data, false);
    }

    public static AppResult Status401Unauthorized(string? message = AppResultMessage.Status401Unauthorized, object? data = null)
    {
        return new AppResult(StatusCodes.Status401Unauthorized, message, data, false);
    }

    public static AppResult Status403Forbidden(string? message = AppResultMessage.Status403Forbidden, object? data = null)
    {
        return new AppResult(StatusCodes.Status403Forbidden, message, data, false);
    }

    public static AppResult Status404NotFound(string? message = AppResultMessage.Status404NotFound, object? data = null)
    {
        return new AppResult(StatusCodes.Status404NotFound, message, data, false);
    }
    public static AppResult Status409Conflict(string? message = AppResultMessage.Status409Conflict, object? data = null)
    {
        return new AppResult(StatusCodes.Status409Conflict, message, data, false);
    }

    public static AppResult Status500InternalServerError(string? message = AppResultMessage.Status500InternalServerError, object? data = null)
    {
        return new AppResult(StatusCodes.Status500InternalServerError, message, data, false);
    }
}
