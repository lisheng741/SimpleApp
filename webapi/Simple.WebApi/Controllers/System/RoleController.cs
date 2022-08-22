using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysRole/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<AppResult> List()
    {
        List<RoleModel> data = await _roleService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<RoleModel> data = await _roleService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] RoleModel model)
    {
        await _roleService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] RoleModel model)
    {
        await _roleService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _roleService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    [HttpGet]
    public async Task<AppResult> OwnMenu(Guid id)
    {
        var data = await _roleService.GetRoleMenuIdsAsync(id);
        return AppResult.Status200OK(data: data);
    }
}
