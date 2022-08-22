namespace Simple.Services;

public class DictionaryTreeModel : ModelBase
{
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";

    public List<DictionaryTreeModel> Children { get; set; } = new List<DictionaryTreeModel>();

    public override void ConfigureMapper(Profile profile)
    {
        profile.CreateMap<SysDictionaryItem, DictionaryTreeModel>();
        profile.CreateMap<SysDictionary, DictionaryTreeModel>()
            .ForMember(d => d.Children, options => options.MapFrom(s => s.DictionaryItems));
    }
}
