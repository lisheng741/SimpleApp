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
    public async Task<AppResult> List([FromQuery] MenuInputModel input)
    {
        List<MenuTreeNodeModel> data = await _menuService.GetAsync(input);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 菜单查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<MenuModel> data = await _menuService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 菜单树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Tree()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(false);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 菜单授权树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> TreeForGrant()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(true);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 菜单增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] MenuModel model)
    {
        await _menuService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 菜单编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] MenuModel model)
    {
        await _menuService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 菜单删除
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] IdInputModel model)
    {
        await _menuService.DeleteAsync(model.Id);
        return AppResult.Status200OK("删除成功");
    }
}
