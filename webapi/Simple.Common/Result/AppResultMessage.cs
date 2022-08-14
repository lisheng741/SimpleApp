namespace Simple.Common;

public class AppResultMessage
{
    public const string Status200OK = "成功";
    public const string Status400BadRequest = "错误的请求";
    public const string Status401Unauthorized = "没有登录状态";
    public const string Status403Forbidden = "缺少权限";
    public const string Status404NotFound = "找不到资源";
    public const string Status409Conflict = "存在冲突";
    public const string Status500InternalServerError = "服务器错误";
}
