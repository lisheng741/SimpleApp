namespace AutoMapper;

public interface IConfigureMapper
{
    /// <summary>
    /// 配置 AutoMapper
    /// </summary>
    /// <param name="profile"></param>
    void ConfigureMapper(Profile profile);
}
