namespace Simple.Common.DependencyInjection;

/// <summary>
/// 生命周期类型
/// </summary>
public enum LifecycleType
{
    Transient,
    Scoped,
    Singleton
}

/// <summary>
/// 自动注入规则
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class AutoInjectionAttribute : Attribute
{
    /// <summary>
    /// 生命周期类型
    /// </summary>
    public LifecycleType Lifecycle { get; set; } = LifecycleType.Transient;

    /// <summary>
    /// 是否自动注册
    /// </summary>
    public bool AutoRegister { get; set; } = true;

    public AutoInjectionAttribute() { }

    public AutoInjectionAttribute(bool autoRegister)
    {
        AutoRegister = autoRegister;
    }

    public AutoInjectionAttribute(LifecycleType lifecycle)
    {
        Lifecycle = lifecycle;
    }
}
