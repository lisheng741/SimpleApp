using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 角色管理
/// </summary>
[Route("api/SysRole/[action]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 角色列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> List()
    {
        List<RoleModel> data = await _roleService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 角色查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<RoleModel> data = await _roleService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 角色增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] RoleModel model)
    {
        await _roleService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 角色编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] RoleModel model)
    {
        await _roleService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 角色删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] IdInputModel models)
    {
        await _roleService.DeleteAsync(models.Id);
        return AppResult.Status200OK("删除成功");
    }

    /// <summary>
    /// 角色拥有菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> OwnMenu(Guid id)
    {
        var data = await _roleService.GetMenuIdsAsync(id);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 角色授权菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> GrantMenu(RoleGrantMenuInputModel input)
    {
        await _roleService.SetMenuAsync(input.Id, input.GrantMenuIdList);
        return AppResult.Status200OK("授权成功");
    }

    /// <summary>
    /// 角色拥有数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> OwnData(Guid id)
    {
        var data = await _roleService.GetDataScopeIdsAsync(id);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 角色授权数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> GrantData(RoleGrantDataScopeInputModel input)
    {
        await _roleService.SetDataScopeAsync(input.Id, input.DataScopeType, input.GrantOrgIdList);
        return AppResult.Status200OK("授权成功");
    }
}
