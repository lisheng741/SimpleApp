namespace Simple.Common.Quartz.Models;

public class TriggerInfo
{
    public virtual string Name { get; private set; }

    public virtual string Cron { get; private set; }

    public TriggerInfo(string name, string cron)
    {
        Name = name;
        Cron = cron;
    }
}
