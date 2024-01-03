namespace AutoMapper;

public interface IMapperConfiguration
{
    /// <summary>
    /// 配置 AutoMapper
    /// </summary>
    /// <param name="profile"></param>
    void Configure(Profile profile);
}
