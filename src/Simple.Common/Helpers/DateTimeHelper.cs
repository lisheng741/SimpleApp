namespace Simple.Common.Helpers;

public static class DateTimeHelper
{
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
