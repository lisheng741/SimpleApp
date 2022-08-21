using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysApp/[action]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private readonly ApplicationService _applicationService;

    public ApplicationController(ApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet]
    public async Task<AppResult> List()
    {
        List<ApplicationModel> data = await _applicationService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<ApplicationModel> data = await _applicationService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] ApplicationModel model)
    {
        await _applicationService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] ApplicationModel model)
    {
        await _applicationService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _applicationService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    [HttpPost]
    public async Task<AppResult> SetAsDefault(IdInputModel id)
    {
        await _applicationService.SetDefault(id.Id);
        return AppResult.Status200OK("设置成功");
    }
}
