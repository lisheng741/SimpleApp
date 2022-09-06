using System.Text.Json;

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
            if (_serializerOptions == null) throw new NullReferenceException(nameof(SerializerOptions));
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

    public static string Serialize<TValue>(TValue value)
    {
        return JsonSerializer.Serialize(value, _serializerOptions);
    }

    public static byte[] SerializeToUtf8Bytes<TValue>(TValue value)
    {
        return JsonSerializer.SerializeToUtf8Bytes(value, _serializerOptions);
    }

    public static TValue? Deserialize<TValue>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<TValue>(json, options ?? SerializerOptions);
    }

    public static object? Deserialize(string json, Type returnType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize(json, returnType, options ?? SerializerOptions);
    }

    public static TValue? Deserialize<TValue>(ReadOnlySpan<byte> utf8Json)
    {
        if (utf8Json == null)
        {
            return default(TValue);
        }
        return JsonSerializer.Deserialize<TValue>(utf8Json, SerializerOptions);
    }
}
