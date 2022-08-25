using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<AppResult> ChangeStatus([FromBody] UserChangeStatusModel input)
    {
        await _userService.ChangeStatusAsync(input);
        return AppResult.Status200OK("更新成功");
    }

    [HttpGet]
    public async Task<AppResult> Detail(Guid id)
    {
        var data = await _userService.GetUserInfoAsync(id);
        return AppResult.Status200OK(data: data);
    }
}
