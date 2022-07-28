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
/// 自动注入规则（只能注释类/接口，不允许相同标签，不允许继承）
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
public class AutoInjectionAttribute : Attribute
{
    /// <summary>
    /// 生命周期类型。默认：瞬态（Transient）
    /// </summary>
    public LifecycleType Lifecycle { get; set; } = LifecycleType.Transient;

    /// <summary>
    /// 是否自动注册。默认：是
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
