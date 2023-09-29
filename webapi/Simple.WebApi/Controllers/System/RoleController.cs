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
    public async Task<List<RoleModel>> List()
    {
        List<RoleModel> data = await _roleService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 角色查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<RoleModel>> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<RoleModel> data = await _roleService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 角色增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] RoleModel model)
    {
        var data = await _roleService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 角色编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] RoleModel model)
    {
        var data = await _roleService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 角色删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] IdInputModel models)
    {
        var data = await _roleService.DeleteAsync(models.Id);
        return ResultHelper.Result200OK("删除成功", data);
    }

    /// <summary>
    /// 角色拥有菜单
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Guid>> OwnMenu(Guid id)
    {
        var data = await _roleService.GetMenuIdsAsync(id);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 角色授权菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> GrantMenu(RoleGrantMenuInputModel input)
    {
        var data = await _roleService.SetMenuAsync(input.Id, input.GrantMenuIdList);
        return ResultHelper.Result200OK("授权成功", data);
    }

    /// <summary>
    /// 角色拥有数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Guid>> OwnData(Guid id)
    {
        var data = await _roleService.GetDataScopeIdsAsync(id);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 角色授权数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> GrantData(RoleGrantDataScopeInputModel input)
    {
        var data = await _roleService.SetDataScopeAsync(input.Id, input.DataScopeType, input.GrantOrgIdList);
        return ResultHelper.Result200OK("授权成功", data);
    }
}
