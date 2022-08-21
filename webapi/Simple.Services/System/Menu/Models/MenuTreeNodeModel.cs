using System.Collections;

namespace Simple.Services;

public class MenuTreeNodeModel : MenuModel, ITreeNode
{
    /// <summary>
    /// 主键
    /// </summary>
    public new Guid Id { get; set; }

    /// <summary>
    /// 父级Id
    /// </summary>
    public Guid ParentId { get; set; }

    public List<MenuTreeNodeModel> Children { get; set; } = new List<MenuTreeNodeModel>();

    public void SetChildren(IList children)
    {
        var nodes = children as List<MenuTreeNodeModel>;
        Children = nodes != null ? nodes : new List<MenuTreeNodeModel>();
    }

    public static TreeBuilder<MenuTreeNodeModel> CreateBuilder(List<MenuTreeNodeModel> nodes)
    {
        return new TreeBuilder<MenuTreeNodeModel>(nodes);
    }

    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysMenu, MenuTreeNodeModel>()
            .ForMember(d => d.Status, options => options.MapFrom(s => s.IsEnabled ? 1 : 0));

        profile.CreateMap<MenuTreeNodeModel, SysMenu>()
            .ForMember(d => d.Id, options => options.Ignore())
            .ForMember(d => d.IsEnabled, options => options.MapFrom(s => s.Status == 1));
    }
}
