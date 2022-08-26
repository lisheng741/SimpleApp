using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysRole/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;
    private readonly RoleMenuService _roleMenuService;
    private readonly RoleDataScopeService _roleDataScopeService;

    public RoleController(RoleService roleService, 
                          RoleMenuService roleMenuService, 
                          RoleDataScopeService roleDataScopeService)
    {
        _roleService = roleService;
        _roleMenuService = roleMenuService;
        _roleDataScopeService = roleDataScopeService;
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
    public async Task<AppResult> Delete([FromBody] IdInputModel models)
    {
        await _roleService.DeleteAsync(models.Id);
        return AppResult.Status200OK("删除成功");
    }

    [HttpGet]
    public async Task<AppResult> OwnMenu(Guid id)
    {
        var data = await _roleMenuService.GetMenuAsync(id);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> GrantMenu(RoleGrantMenuInputModel input)
    {
        await _roleMenuService.SetMenuAsync(input.Id, input.GrantMenuIdList);
        return AppResult.Status200OK("授权成功");
    }

    [HttpGet]
    public async Task<AppResult> OwnData(Guid id)
    {
        var data = await _roleDataScopeService.GetDataScopeAsync(id);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> GrantData(RoleGrantDataScopeInputModel input)
    {
        await _roleDataScopeService.SetDataScopeAsync(input.Id, input.GrantOrgIdList);
        return AppResult.Status200OK("授权成功");
    }
}
