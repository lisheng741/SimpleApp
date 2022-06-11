namespace SimpleApp.Common.Helpers;

public static class DateHelper
{
    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到秒
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static long ToUnixTimeSeconds(DateTime? dt = null)
    {
        if (dt == null) dt = DateTime.Now;
        DateTimeOffset dto = new DateTimeOffset(dt.Value);
        return dto.ToUnixTimeSeconds();
    }

    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到毫秒
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static long ToUnixTimeMilliseconds(DateTime? dt = null)
    {
        if (dt == null) dt = DateTime.Now;
        DateTimeOffset dto = new DateTimeOffset(dt.Value);
        return dto.ToUnixTimeMilliseconds();
    }
}
