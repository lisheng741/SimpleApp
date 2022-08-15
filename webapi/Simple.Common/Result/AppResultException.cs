using Microsoft.AspNetCore.Http;

namespace Simple.Common;

/// <summary>
/// 返回统一结果的异常
/// </summary>
public partial class AppResultException : Exception
{
    /// <summary>
    /// 结果信息
    /// </summary>
    public AppResult AppResult { get; private set; }

    /// <summary>
    /// 源异常
    /// </summary>
    public Exception? SourceException { get; private set; }

    public AppResultException()
        : this(new AppResult(), null)
    {
    }

    public AppResultException(AppResult result)
        : this(result, null)
    {
    }

    public AppResultException(Exception exception)
        : this(new AppResult(), exception)
    {
    }

    public AppResultException(AppResult result, Exception? exception)
    {
        AppResult = result;
        SourceException = exception;
    }
}

public partial class AppResultException
{
    public static AppResultException Status200OK(string? message = AppResultMessage.Status200OK, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status200OK, message, data, true);
        return new AppResultException(appResult);
    }

    public static AppResultException Status400BadRequest(string? message = AppResultMessage.Status400BadRequest, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status400BadRequest, message, data, false);
        return new AppResultException(appResult);
    }

    public static AppResultException Status401Unauthorized(string? message = AppResultMessage.Status401Unauthorized, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status401Unauthorized, message, data, false);
        return new AppResultException(appResult);
    }

    public static AppResultException Status403Forbidden(string? message = AppResultMessage.Status403Forbidden, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status403Forbidden, message, data, false);
        return new AppResultException(appResult);
    }

    public static AppResultException Status404NotFound(string? message = AppResultMessage.Status404NotFound, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status404NotFound, message, data, false);
        return new AppResultException(appResult);
    }
    public static AppResultException Status409Conflict(string? message = AppResultMessage.Status409Conflict, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status409Conflict, message, data, false);
        return new AppResultException(appResult);
    }

    public static AppResultException Status500InternalServerError(string? message = AppResultMessage.Status500InternalServerError, object? data = null)
    {
        var appResult = new AppResult(StatusCodes.Status500InternalServerError, message, data, false);
        return new AppResultException(appResult);
    }
}
