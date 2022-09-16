using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 机构管理
/// </summary>
[Route("api/SysOrg/[action]")]
[ApiController]
[Authorize]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationService _organizationService;

    public OrganizationController(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// 机构列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> List()
    {
        List<OrganizationModel> data = await _organizationService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 机构查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] OrganizationPageInputModel model)
    {
        PageResultModel<OrganizationModel> data = await _organizationService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 机构树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Tree()
    {
        List<AntTreeNode> data = await _organizationService.GetTreeAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 机构增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] OrganizationModel model)
    {
        await _organizationService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 机构编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] OrganizationModel model)
    {
        await _organizationService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 机构删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _organizationService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }
}
