namespace Simple.Services;

public abstract class ModelBase : IConfigureMapper
{
    public virtual void ConfigureMapper(Profile profile)
    {
    }
}
