using System.Text.Json;
using System.Text.Json.Serialization;

namespace Simple.Common.Json.SystemTextJson;

/// <summary>
/// DateTime 格式转换
/// </summary>
public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    /// <summary>
    /// DateTime 格式
    /// </summary>
    public string Format { get; }

    /// <summary>
    /// DateTime 格式转换
    /// </summary>
    /// <param name="format">DateTime 格式</param>
    public DateTimeJsonConverter(string format = "yyyy-MM-dd HH:mm:ss")
    {
        Format = format;
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        return reader.GetDateTime();
    }

    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        //这里把时间格式做一个自顶一格式的转换就行
        writer.WriteStringValue(value.ToString(Format));
    }
}
