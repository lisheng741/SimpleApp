using Microsoft.AspNetCore.Authorization;
using Simple.Common.Filters;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 异常日志
/// </summary>
[Route("api/sysExLog/[action]")]
[ApiController]
[Authorize]
[DisabledRequestRecord]
public class LogExceptionController : ControllerBase
{
    private readonly LogExceptionService _exceptionLogService;

    public LogExceptionController(LogExceptionService exceptionLogService)
    {
        _exceptionLogService = exceptionLogService;
    }

    /// <summary>
    /// 异常日志查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<LogExceptionModel>> Page([FromQuery] LogPageInputModel model)
    {
        return await _exceptionLogService.GetPageAsync(model);
    }
}
