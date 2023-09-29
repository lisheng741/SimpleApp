using Microsoft.AspNetCore.Authorization;
using Simple.Common.Filters;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 操作日志
/// </summary>
[Route("api/sysOpLog/[action]")]
[ApiController]
[Authorize]
[DisabledRequestRecord]
public class LogOperatingController : ControllerBase
{
    private readonly LogOperatingService _logOperatingService;

    public LogOperatingController(LogOperatingService logOperatingService)
    {
        _logOperatingService = logOperatingService;
    }

    /// <summary>
    /// 操作日志查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<LogOperatingModel>> Page([FromQuery] LogPageInputModel model)
    {
        return await _logOperatingService.GetPageAsync(model);
    }
}
