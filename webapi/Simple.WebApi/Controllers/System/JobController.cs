using Microsoft.AspNetCore.Authorization;

namespace Simple.WebApi.Controllers.System;

[Route("api/sysTimers/[action]")]
[ApiController]
// [Authorize]
public class JobController
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }


    [HttpGet]
    public async Task<AppResult> List()
    {
        List<JobModel> data = await _jobService.GetAsync();
        return AppResult.Status200OK(data: data);
    }


    [HttpGet]
    public async Task<AppResult> Page([FromQuery] JobPageInputModel model)
    {
        PageResultModel<JobModel> data = await _jobService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }


    [HttpPost]
    public async Task<AppResult> Add([FromBody] JobModel model)
    {
        await _jobService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }


    [HttpPost]
    public async Task<AppResult> Edit([FromBody] JobModel model)
    {
        await _jobService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }


    [HttpPost]
    public async Task<AppResult> Delete([FromBody] IdInputModel model)
    {
        await _jobService.DeleteAsync(model.Id);
        return AppResult.Status200OK("删除成功");
    }


    [HttpPost]
    public async Task<AppResult> Start([FromBody] IdInputModel model)
    {
        await _jobService.SetEnabledAsync(model.Id, true);
        return AppResult.Status200OK("启动成功");
    }


    [HttpPost]
    public async Task<AppResult> Stop([FromBody] IdInputModel model)
    {
        await _jobService.SetEnabledAsync(model.Id, false);
        return AppResult.Status200OK("停止成功");
    }

    [HttpGet]
    public async Task<AppResult> GetActionClasses()
    {
        var data = await _jobService.GetActionClass();
        return AppResult.Status200OK(data: data);
    }
}
