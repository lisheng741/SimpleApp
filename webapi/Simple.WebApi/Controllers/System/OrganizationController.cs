using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysOrg/[action]")]
[ApiController]
public class OrganizationController : ControllerBase
{
    private readonly OrganizationService _organizationService;

    public OrganizationController(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    public async Task<AppResult> List()
    {
        List<OrganizationModel> organizations = await _organizationService.GetAsync();
        return AppResult.Status200OK(data: organizations);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] OrganizationPageInputModel model)
    {
        PageResultModel<OrganizationModel> data = await _organizationService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Tree()
    {
        List<AntTreeNode> data = await _organizationService.GetTreeAsync();
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] OrganizationModel model)
        => await _organizationService.AddAsync(model);

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] OrganizationModel model)
        => await _organizationService.UpdateAsync(model);

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<OrganizationModel> models)
        => await _organizationService.DeleteAsync(models);
}
