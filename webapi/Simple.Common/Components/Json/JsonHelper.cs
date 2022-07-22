using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Simple.Common.Components.Json;

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
        set
        {
            if (_serializerOptions != null) throw new Exception($"{nameof(SerializerOptions)}只能设置一次");
            _serializerOptions = value ?? throw new ArgumentNullException(nameof(value));
        }
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
