using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 菜单管理
/// </summary>
[Route("api/SysMenu/[action]")]
[ApiController]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 菜单列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<MenuTreeNodeModel>> List([FromQuery] MenuInputModel input)
    {
        List<MenuTreeNodeModel> data = await _menuService.GetAsync(input);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 菜单查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<MenuModel>> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<MenuModel> data = await _menuService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 菜单树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<AntTreeNode>> Tree()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(false);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 菜单授权树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<AntTreeNode>> TreeForGrant()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(true);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 菜单增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] MenuModel model)
    {
        var data = await _menuService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 菜单编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] MenuModel model)
    {
        var data = await _menuService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 菜单删除
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] IdInputModel model)
    {
        var data = await _menuService.DeleteAsync(model.Id);
        return ResultHelper.Result200OK("删除成功", data);
    }
}
