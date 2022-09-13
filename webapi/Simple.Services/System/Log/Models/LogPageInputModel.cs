namespace Simple.Services;

public class LogPageInputModel : PageInputModel
{
    public Guid? EventId { get; set; }

    public string? Success { get; set; }

    public DateTime? SearchBeginTime { get; set; }

    public DateTime? SearchEndTime { get; set; }
}
