﻿namespace Simple.Services;

/// <summary>
/// 菜单
/// </summary>
public class MenuModel : ModelBase
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid Pid { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [MaxLength(128)]
    public string Code { get; set; } = "";

    /// <summary>
    /// 名称
    /// </summary>
    [MaxLength(128)]
    public string Name { get; set; } = "";

    /// <summary>
    /// 应用分类
    /// </summary>
    [MaxLength(128)]
    public string? Application { get; set; }

    /// <summary>
    /// 菜单类型（0-目录，1-菜单，2-按钮）
    /// </summary>
    public MenuType Type { get; set; } = MenuType.Directory;

    /// <summary>
    /// 打开方式（0-无，1-组件，2-内链，3-外链）
    /// </summary>
    public MenuOpenType OpenType { get; set; } = MenuOpenType.None;

    /// <summary>
    /// 图标
    /// </summary>
    [MaxLength(128)]
    public string? Icon { get; set; }

    /// <summary>
    /// 前端组件
    /// </summary>
    [MaxLength(256)]
    public string? Component { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [MaxLength(256)]
    public string? Router { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    [MaxLength(256)]
    public string? Permission { get; set; }

    /// <summary>
    /// 是否可见（Y-是，N-否）
    /// </summary>
    public string Visible { get; set; } = "Y";

    /// <summary>
    /// 内链地址
    /// </summary>
    [MaxLength(1024)]
    public string? Link { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [MaxLength(2048)]
    public string? Redirect { get; set; }

    /// <summary>
    /// 权重（1-系统权重，2-业务权重）
    /// </summary>
    public MenuWeightType Weight { get; set; } = MenuWeightType.System;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(2048)]
    public string? Remark { get; set; }

    /// <summary>
    /// 启用状态（1-启用，0-禁用）
    /// </summary>
    public int Status { get; set; } = 1;


    public override void Configure(Profile profile)
    {
        profile.CreateMap<SysMenu, MenuModel>()
            .ForMember(d => d.Pid, options => options.MapFrom(s => s.ParentId))
            .ForMember(d => d.Status, options => options.MapFrom(s => s.IsEnabled ? 1 : 0));

        profile.CreateMap<MenuModel, SysMenu>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.ParentId, options => options.MapFrom(s => s.Pid))
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1));
    }
}
