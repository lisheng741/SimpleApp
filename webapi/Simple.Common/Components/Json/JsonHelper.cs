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
}
