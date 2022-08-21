namespace Simple.Services;

public class OrganizationPageInputModel : PageInputModel
{
    public override string? Name { get; set; }
    public Guid? Pid { get; set; }
}
