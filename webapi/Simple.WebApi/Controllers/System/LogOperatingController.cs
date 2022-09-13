using Simple.Common.Filters;

namespace Simple.WebApi.Controllers.System;

[Route("api/sysOpLog/[action]")]
[ApiController]
[DisabledRequestRecord]
public class LogOperatingController : ControllerBase
{
    private readonly LogOperatingService _logOperatingService;

    public LogOperatingController(LogOperatingService logOperatingService)
    {
        _logOperatingService = logOperatingService;
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] LogPageInputModel model)
    {
        PageResultModel<LogOperatingModel> data = await _logOperatingService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }
}
