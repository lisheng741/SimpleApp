namespace Microsoft.AspNetCore.Http;

public static class HttpContextExtensions
{
    /// <summary>
    /// 获取请求的ip4
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetRequestIPv4(this HttpContext context)
    {
        string ip = string.Empty;
        if (context.Connection.RemoteIpAddress != null)
        {
            if (context.Request.Headers.ContainsKey("X-Real-IP"))
            {
                ip = context.Request.Headers["X-Real-IP"].FirstOrDefault() ?? "";
            }
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "";
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";
            }
        }
        return ip;
    }
}
