using Simple.Common.Filters;

namespace Simple.WebApi.Controllers.System;

[Route("api/sysExLog/[action]")]
[ApiController]
[DisabledRequestRecord]
public class LogExceptionController : ControllerBase
{
    private readonly LogExceptionService _exceptionLogService;

    public LogExceptionController(LogExceptionService exceptionLogService)
    {
        _exceptionLogService = exceptionLogService;
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] LogPageInputModel model)
    {
        PageResultModel<LogExceptionModel> data = await _exceptionLogService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }
}
