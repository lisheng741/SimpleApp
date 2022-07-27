using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System;

public static class DateTimeExtensions
{
    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return DateTimeHelper.ToUnixTimeSeconds(dateTime);
    }

    /// <summary>
    /// 时间转时间戳Unix-时间戳精确到毫秒
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
    {
        return DateTimeHelper.ToUnixTimeMilliseconds(dateTime);
    }
}
