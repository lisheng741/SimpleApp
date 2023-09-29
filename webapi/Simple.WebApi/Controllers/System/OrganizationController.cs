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
    public async Task<List<OrganizationModel>> List()
    {
        List<OrganizationModel> data = await _organizationService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 机构查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<OrganizationModel>> Page([FromQuery] OrganizationPageInputModel model)
    {
        PageResultModel<OrganizationModel> data = await _organizationService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 机构树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<AntTreeNode>> Tree()
    {
        List<AntTreeNode> data = await _organizationService.GetTreeAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 机构增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] OrganizationModel model)
    {
        var data = await _organizationService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 机构编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] OrganizationModel model)
    {
        var data = await _organizationService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 机构删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] List<IdInputModel> models)
    {
        var data = await _organizationService.DeleteAsync(models.Select(m => m.Id));
        return ResultHelper.Result200OK("删除成功", data);
    }
}
