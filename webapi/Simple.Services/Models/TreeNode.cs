using System.Collections;
using Pipelines.Sockets.Unofficial.Arenas;

namespace Simple.Services;

public interface ITreeNode
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }

    public void SetChildren(IList children);
}

public class TreeNode : ITreeNode
{
    public virtual Guid Id { get; set; }
    public virtual Guid ParentId { get; set; }
    public virtual string Name { get; set; }

    public TreeNode(Guid id, string name, Guid? parentId = null)
    {
        Id = id;
        Name = name;
        ParentId = parentId.HasValue ? parentId.Value : Guid.Empty;
    }

    public virtual void SetChildren(IList children)
    {
    }
}

public class TreeBuilder<TNode>
    where TNode : ITreeNode
{
    public Guid RootId { get; set; }
    public List<TNode> Nodes { get; }

    public TreeBuilder(List<TNode> nodes)
    : this(nodes, Guid.Empty)
    {
    }

    public TreeBuilder(List<TNode> nodes, Guid rootId)
    {
        Nodes = nodes;
        RootId = rootId;
    }

    public List<TNode> Build()
    {
        Nodes.ForEach(p => p.SetChildren(Nodes.Where(n => n.ParentId == p.Id).ToList()));
        return Nodes.Where(n => n.ParentId == RootId).ToList();
    }
}
