using System.ComponentModel.DataAnnotations;

namespace Simple.Common.Models;

public class ExceptionEvent : EventModel
{
    /// <summary>
    /// 操作人账号
    /// </summary>
    [MaxLength(64)]
    public string? Account { get; set; }

    /// <summary>
    /// 异常名称
    /// </summary>
    [MaxLength(128)]
    public string? Name { get; set; }

    /// <summary>
    /// 异常消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 类名称
    /// </summary>
    [MaxLength(256)]
    public string? ClassName { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    [MaxLength(256)]
    public string? MethodName { get; set; }

    /// <summary>
    /// 异常源
    /// </summary>
    public string? ExceptionSource { get; set; }

    /// <summary>
    /// 堆栈信息
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string? Parameters { get; set; }

    /// <summary>
    /// 异常时间
    /// </summary>
    public DateTimeOffset ExceptionTime { get; set; }
}
