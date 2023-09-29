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
    public async Task<List<PositionModel>> List()
    {
        List<PositionModel> data = await _positionService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 岗位查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<PositionModel>> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<PositionModel> data = await _positionService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 岗位增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] PositionModel model)
    {
        var data = await _positionService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 岗位编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] PositionModel model)
    {
        var data = await _positionService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 岗位删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] List<IdInputModel> models)
    {
        var data = await _positionService.DeleteAsync(models.Select(m => m.Id));
        return ResultHelper.Result200OK("删除成功", data);
    }
}
