namespace Simple.Common.Quartz.Models;

public class JobInfo
{
    public virtual string Name { get; private set; }
    public virtual IList<TriggerInfo> Triggers { get; private set; } = new List<TriggerInfo>();

    public JobInfo(string name)
    {
        Name = name;
    }
}
