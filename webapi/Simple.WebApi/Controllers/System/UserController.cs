using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysUser/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<AppResult> List()
    {
        List<UserModel> data = await _userService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] UserPageInputModel model)
    {
        PageResultModel<UserModel> data = await _userService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] UserModel model)
    {
        await _userService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] UserModel model)
    {
        await _userService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _userService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    [HttpPost]
    public async Task<AppResult> ChangeStatus([FromBody] UserChangeStatusInputModel input)
    {
        await _userService.SetEnabledAsync(input.Id, input.Status == 1);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> ResetPwd(IdInputModel input)
    {
        var data = await _userService.SetPasswordAsync(input.Id);
        return AppResult.Status200OK("重置成功");
    }

    [HttpGet]
    public async Task<AppResult> OwnRole(Guid id)
    {
        var data = await _userService.GetRoleIdsAsync(id);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> GrantRole(UserGrantRoleInputModel input)
    {
        await _userService.SetRoleAsync(input.Id, input.GrantRoleIdList);
        return AppResult.Status200OK("授权成功");
    }

    [HttpGet]
    public async Task<AppResult> OwnData(Guid id)
    {
        var data = await _userService.GetDataScopesAsync(id);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> GrantData(UserGrantDataScopeInputModel input)
    {
        await _userService.SetDataScopeAsync(input.Id, input.GrantOrgIdList);
        return AppResult.Status200OK("授权成功");
    }
}
