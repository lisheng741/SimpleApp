namespace Simple.Services;

public class AntTreeNode
{
    public virtual Guid Id { get; set; }
    public virtual Guid Value { get; set; }
    public virtual string Title { get; set; }
    public virtual List<AntTreeNode> Children { get; set; } = new List<AntTreeNode>();

    public AntTreeNode(Guid value, string title)
    {
        Id = value;
        Value = value;
        Title = title;
    }

    public static AntTreeBuilder CreateBuilder(List<TreeNode> nodes)
    {
        return new AntTreeBuilder(nodes);
    }
}

public class AntTreeBuilder
{
    public Guid RootId { get; set; }
    public List<TreeNode> Nodes { get; }

    public AntTreeBuilder(List<TreeNode> nodes)
        : this(nodes, Guid.Empty)
    {
    }

    public AntTreeBuilder(List<TreeNode> nodes, Guid rootId)
    {
        Nodes = nodes;
        RootId = rootId;
    }

    public List<AntTreeNode> Build()
    {
        List<AntTreeNode> nodes = Convert(Nodes.Where(n => n.ParentId == RootId));
        nodes.ForEach(n => BuildChildren(n));
        return nodes;
    }

    private void BuildChildren(AntTreeNode parent)
    {
        parent.Children = Convert(Nodes.Where(n => n.ParentId == parent.Value));
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
        return new AntTreeNode(node.Id, node.Name);
    }
}
