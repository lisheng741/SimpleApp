using System.Text.Json.Serialization;
using Simple.Common.Json.SystemTextJson;

namespace System.Text.Json;

public static class JsonConverterExtensions
{
    /// <summary>
    /// 添加 DateTime 转化器
    /// </summary>
    /// <param name="converters"></param>
    /// <param name="formatString">DateTime 格式化字符串</param>
    public static void AddDateTimeJsonConverter(this IList<JsonConverter> converters, string formatString = "yyyy-MM-dd HH:mm:ss")
    {
        converters.Add(new DateTimeJsonConverter(formatString));
    }
}


