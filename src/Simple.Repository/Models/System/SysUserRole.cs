namespace Simple.Repository.Models.System;

public class SysUserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual SysUser User { get; set; } = default!;
    public virtual SysRole Role { get; set; } = default!;
}
