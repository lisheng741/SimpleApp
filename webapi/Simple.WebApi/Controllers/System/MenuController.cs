using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysMenu/[action]")]
[ApiController]
public class MenuController : ControllerBase
{
    private readonly MenuService _menuService;

    public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<AppResult> List([FromQuery] MenuInputModel input)
    {
        List<MenuTreeNodeModel> data = await _menuService.GetAsync(input);
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<MenuModel> data = await _menuService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Tree()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(false);
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> TreeForGrant()
    {
        List<AntTreeNode> data = await _menuService.GetTreeAsync(true);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] MenuModel model)
    {
        await _menuService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] MenuModel model)
    {
        await _menuService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] IdInputModel model)
    {
        await _menuService.DeleteAsync(model.Id);
        return AppResult.Status200OK("删除成功");
    }
}
