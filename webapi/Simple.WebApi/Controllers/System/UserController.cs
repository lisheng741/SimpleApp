using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple.Services;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/SysUser/[action]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 用户列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> List()
    {
        List<UserModel> data = await _userService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 用户查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] UserPageInputModel model)
    {
        PageResultModel<UserModel> data = await _userService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 用户增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] UserModel model)
    {
        await _userService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 用户编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] UserModel model)
    {
        await _userService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 用户删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _userService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    /// <summary>
    /// 用户修改状态
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> ChangeStatus([FromBody] UserChangeStatusInputModel input)
    {
        await _userService.SetEnabledAsync(input.Id, input.Status == 1);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 用户修改密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> UpdatePwd(UserChangePasswordInputModel input)
    {
        await _userService.UpdatePasswordAsync(input.Id, input.Password, input.NewPassword);
        return AppResult.Status200OK("修改成功");
    }

    /// <summary>
    /// 用户重置密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> ResetPwd(IdInputModel input)
    {
        var data = await _userService.SetPasswordAsync(input.Id);
        return AppResult.Status200OK("重置成功");
    }

    /// <summary>
    /// 用户拥有角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> OwnRole(Guid id)
    {
        var data = await _userService.GetRoleIdsAsync(id);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 用户授权角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> GrantRole(UserGrantRoleInputModel input)
    {
        await _userService.SetRoleAsync(input.Id, input.GrantRoleIdList);
        return AppResult.Status200OK("授权成功");
    }

    /// <summary>
    /// 用户拥有数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> OwnData(Guid id)
    {
        var data = await _userService.GetDataScopesAsync(id);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 用户授权数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> GrantData(UserGrantDataScopeInputModel input)
    {
        await _userService.SetDataScopeAsync(input.Id, input.GrantOrgIdList);
        return AppResult.Status200OK("授权成功");
    }
}
