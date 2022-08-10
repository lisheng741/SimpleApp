using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Common.Helpers;

public static class ActivatorHelper
{
    /// <summary>
    /// 创建实例.
    /// 如果没有公共的构造函数则返回 null, 如果是带参的构造函数则传入默认参数.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object? CreateInstance(Type type)
    {
        var constructor = type.GetConstructors()
            .Where(c => c.IsPublic)
            .OrderBy(c => c.GetParameters().Length)
            .FirstOrDefault();

        if(constructor == null) return null;

        // 获取需要的参数列表
        var parameters = new List<object?>();
        foreach(var parameter in constructor.GetParameters())
        {
            // 如果有默认值，则获取默认值
            if (parameter.HasDefaultValue)
            {
                parameters.Add(parameter.DefaultValue);
                continue;
            }

            // 如果是值类型，则创建默认值；非值类型的默认值直接为 null
            var parameterType = parameter.ParameterType;
            object? value = parameterType.IsValueType ? CreateInstance(parameterType) : null;
            parameters.Add(value);
        }

        return Activator.CreateInstance(type, parameters.ToArray());
    }
}
