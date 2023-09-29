using Microsoft.AspNetCore.Authorization;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 定时任务
/// </summary>
[Route("api/sysTimers/[action]")]
[ApiController]
[Authorize]
public class JobController
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    /// <summary>
    /// 定时任务列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<JobModel>> List()
    {
        List<JobModel> data = await _jobService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 定时任务查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<JobModel>> Page([FromQuery] JobPageInputModel model)
    {
        PageResultModel<JobModel> data = await _jobService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 定时任务增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] JobModel model)
    {
        var data = await _jobService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 定时任务编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] JobModel model)
    {
        var data = await _jobService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 定时任务删除
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] IdInputModel model)
    {
        var data = await _jobService.DeleteAsync(model.Id);
        return ResultHelper.Result200OK("删除成功", data);
    }

    /// <summary>
    /// 定时任务启动
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Start([FromBody] IdInputModel model)
    {
        var data = await _jobService.SetEnabledAsync(model.Id, true);
        return ResultHelper.Result200OK("启动成功", data);
    }

    /// <summary>
    /// 定时任务暂停
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Stop([FromBody] IdInputModel model)
    {
        var data = await _jobService.SetEnabledAsync(model.Id, false);
        return ResultHelper.Result200OK("停止成功", data);
    }

    /// <summary>
    /// 定时任务可执行列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<string>> GetActionClasses()
    {
        return await _jobService.GetActionClass();
    }
}
