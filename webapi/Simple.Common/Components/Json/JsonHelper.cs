using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simple.Common.Helpers;

public static class JsonHelper
{
    private static JsonSerializerOptions? _serializerOptions;

    /// <summary>
    /// 获取序列化/反序列化Json的配置
    /// </summary>
    public static JsonSerializerOptions SerializerOptions
    {
        get
        {
            if (_serializerOptions == null) throw new ArgumentNullException(nameof(SerializerOptions));
            return _serializerOptions;
        }
    }

    /// <summary>
    /// 设置序列化/反序列化Json的配置
    /// </summary>
    /// <param name="serializerOptions"></param>
    /// <exception cref="Exception"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public static void Configure(JsonSerializerOptions serializerOptions)
    {
        if (_serializerOptions != null)
        {
            throw new Exception($"{nameof(SerializerOptions)}不可修改！");
        }
        _serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
    }

    public static byte[] Serialize<T>(T obj)
    {
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj, SerializerOptions));
    }

    public static T? Deserialize<T>(byte[] bytes)
    {
        if (bytes == null)
        {
            return default(T);
        }
        return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(bytes), SerializerOptions);
    }
}
