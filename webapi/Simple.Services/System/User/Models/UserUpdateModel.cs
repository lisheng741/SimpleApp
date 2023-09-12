using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Services;

public class UserUpdateModel : ModelBase
{/// <summary>
 /// 主键
 /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号不能为空"),
        MaxLength(64, ErrorMessage = "账号长度不能超过64个字符")]
    public string Account { get; set; } = "";

    /// <summary>
    /// 姓名
    /// </summary>
    [MaxLength(32)]
    public string? Name { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [MaxLength(16)]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮件
    /// </summary>
    [MaxLength(64)]
    public string? Email { get; set; }

    /// <summary>
    /// 性别：1-男，2-女
    /// </summary>
    public GenderType Sex { get; set; } = GenderType.Unknown;

    /// <summary>
    /// 主岗位Id
    /// </summary>
    public Guid? PositionId { get; set; }

    /// <summary>
    /// 主部门Id
    /// </summary>
    public Guid? OrganizationId { get; set; }

    /// <summary>
    /// 启用状态: 1-启用，0-禁用
    /// </summary>
    public int Status { get; set; } = 1;


    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<UserUpdateModel, SysUser>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.Password, options => options.Ignore())
            .ForMember(d => d.UserName, options => options.MapFrom(s => s.Account))
            .ForMember(d => d.Gender, options => options.MapFrom(s => s.Sex))
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1));
    }
}
