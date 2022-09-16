using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 岗位管理
/// </summary>
[Route("api/SysPos/[action]")]
[ApiController]
[Authorize]
public class PositionController : ControllerBase
{
    private readonly PositionService _positionService;

    public PositionController(PositionService positionService)
    {
        _positionService = positionService;
    }

    /// <summary>
    /// 岗位列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> List()
    {
        List<PositionModel> data = await _positionService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 岗位查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<PositionModel> data = await _positionService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 岗位增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] PositionModel model)
    {
        await _positionService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 岗位编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] PositionModel model)
    {
        await _positionService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 岗位删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _positionService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }
}
