namespace Simple.Services;

public class TreeNode
{
    public virtual Guid Id { get; set; }
    public virtual Guid? ParentId { get; set; }
    public virtual string Name { get; set; }

    public TreeNode(Guid id, string name, Guid? parentId = null)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
    }
}
