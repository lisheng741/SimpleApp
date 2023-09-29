using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 应用管理
/// </summary>
[Route("api/SysApp/[action]")]
[ApiController]
[Authorize]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationService _applicationService;

    public ApplicationController(ApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    /// <summary>
    /// 应用列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<ApplicationModel>> List()
    {
        return await _applicationService.GetAsync();
    }

    /// <summary>
    /// 应用查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<ApplicationModel>> Page([FromQuery] PageInputModel model)
    {
        return await _applicationService.GetPageAsync(model);
    }

    /// <summary>
    /// 应用增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] ApplicationModel model)
    {
        var data = await _applicationService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 应用编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] ApplicationModel model)
    {
        var data = await _applicationService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 应用删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] List<IdInputModel> models)
    {
        var data = await _applicationService.DeleteAsync(models.Select(m => m.Id));
        return ResultHelper.Result200OK("删除成功", data);
    }

    /// <summary>
    /// 设置默认应用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> SetAsDefault(IdInputModel id)
    {
        var data = await _applicationService.SetDefault(id.Id);
        return ResultHelper.Result200OK("设置成功", data);
    }
}
