﻿namespace Simple.Services;

public class UserPageInputModel : PageInputModel
{
    public Guid? OrgId { get; set; }

    public string? SearchValue { get; set; }

    public int? SearchStatus { get; set; }
}