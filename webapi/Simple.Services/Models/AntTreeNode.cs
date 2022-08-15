namespace Simple.Services;

public class AntTreeNode
{
    public virtual Guid Id { get; set; }
    public virtual Guid? ParentId { get; set; }
    public virtual string Title { get; set; }
    public virtual List<AntTreeNode> Children { get; set; } = new List<AntTreeNode>();

    public AntTreeNode(Guid id, string title, Guid? parentId = null)
    {
        Id = id;
        Title = title;
        ParentId = parentId;
    }

    public static AntTreeBuilder CreateBuilder(List<TreeNode> nodes)
    {
        return new AntTreeBuilder(nodes);
    }
}

public class AntTreeBuilder
{
    public List<TreeNode> Nodes { get; }
    public Guid ParentId { get; set; }

    public AntTreeBuilder(List<TreeNode> nodes)
        : this(nodes, Guid.Empty)
    {
    }

    public AntTreeBuilder(List<TreeNode> nodes, Guid parentId)
    {
        Nodes = nodes;
        ParentId = parentId;
    }

    public List<AntTreeNode> Build()
    {
        List<AntTreeNode> parents = Convert(Nodes.Where(n => n.ParentId == ParentId));
        parents.ForEach(p => BuildChildren(p));
        return parents;
    }

    private void BuildChildren(AntTreeNode parent)
    {
        parent.Children = Convert(Nodes.Where(n => n.ParentId == parent.Id));
        parent.Children.ForEach(child => BuildChildren(child));
    }

    private List<AntTreeNode> Convert(IEnumerable<TreeNode> nodes)
    {
        var result = new List<AntTreeNode>();
        foreach (var node in nodes)
        {
            result.Add(Convert(node));
        }
        return result;
    }

    private AntTreeNode Convert(TreeNode node)
    {
        return new AntTreeNode(node.Id, node.Name, node.ParentId);
    }
}
