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
    public async Task<List<UserModel>> List()
    {
        List<UserModel> data = await _userService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 用户查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<UserModel>> Page([FromQuery] UserPageInputModel model)
    {
        PageResultModel<UserModel> data = await _userService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 用户增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] UserAddModel model)
    {
        var data = await _userService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 用户编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] UserUpdateModel model)
    {
        var data = await _userService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 用户删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] List<IdInputModel> models)
    {
        var data = await _userService.DeleteAsync(models.Select(m => m.Id));
        return ResultHelper.Result200OK("删除成功", data);
    }

    /// <summary>
    /// 用户修改状态
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> ChangeStatus([FromBody] UserChangeStatusInputModel input)
    {
        var data = await _userService.SetEnabledAsync(input.Id, input.Status == 1);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 用户修改密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> UpdatePwd(UserChangePasswordInputModel input)
    {
        var data = await _userService.UpdatePasswordAsync(input.Id, input.Password, input.NewPassword);
        return ResultHelper.Result200OK("修改成功", data);
    }

    /// <summary>
    /// 用户重置密码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> ResetPwd(IdInputModel input)
    {
        var data = await _userService.SetPasswordAsync(input.Id);
        return ResultHelper.Result200OK("重置成功", data);
    }

    /// <summary>
    /// 用户拥有角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Guid>> OwnRole(Guid id)
    {
        var data = await _userService.GetRoleIdsAsync(id);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 用户授权角色
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> GrantRole(UserGrantRoleInputModel input)
    {
        var data = await _userService.SetRoleAsync(input.Id, input.GrantRoleIdList);
        return ResultHelper.Result200OK("授权成功", data);
    }

    /// <summary>
    /// 用户拥有数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<Guid>> OwnData(Guid id)
    {
        var data = await _userService.GetDataScopesAsync(id);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 用户授权数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> GrantData(UserGrantDataScopeInputModel input)
    {
        var data = await _userService.SetDataScopeAsync(input.Id, input.GrantOrgIdList);
        return ResultHelper.Result200OK("授权成功", data);
    }
}
