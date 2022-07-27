namespace Simple.Common.Helpers;

public static class DateTimeHelper
{
    /// <summary>
    /// 时间戳转本地时间-时间戳精确到秒
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static DateTime FromUnixTimeSeconds(long seconds)
    {
        var dto = DateTimeOffset.FromUnixTimeSeconds(seconds);
        return dto.ToLocalTime().DateTime;
    }

    /// <summary>
    /// 时间戳转本地时间-时间戳精确到毫秒
    /// </summary>
    /// <returns></returns>
    public static DateTime FromUnixTimeMilliseconds(long milliseconds)
    {

        var dto = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
        return dto.ToLocalTime().DateTime;
    }

    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimeSeconds(DateTime? dateTime = null)
    {
        if (!dateTime.HasValue) dateTime = DateTime.Now;
        DateTimeOffset dto = new DateTimeOffset(dateTime.Value);
        return dto.ToUnixTimeSeconds();
    }

    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到毫秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimeMilliseconds(DateTime? dateTime = null)
    {
        if (!dateTime.HasValue) dateTime = DateTime.Now;
        DateTimeOffset dto = new DateTimeOffset(dateTime.Value);
        return dto.ToUnixTimeMilliseconds();
    }
}
