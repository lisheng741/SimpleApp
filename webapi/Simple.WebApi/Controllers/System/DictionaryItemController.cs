using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 字典值
/// </summary>
[Route("api/SysDictData/[action]")]
[ApiController]
[Authorize]
public class DictionaryItemController : ControllerBase
{
    private readonly DictionaryItemService _dictionaryItemService;

    public DictionaryItemController(DictionaryItemService dictionaryItemService)
    {
        _dictionaryItemService = dictionaryItemService;
    }

    /// <summary>
    /// 字典值列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> List()
    {
        List<DictionaryItemModel> data = await _dictionaryItemService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 字典值查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] DictionaryItemPageInputModel model)
    {
        PageResultModel<DictionaryItemModel> data = await _dictionaryItemService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 字典值增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] DictionaryItemModel model)
    {
        await _dictionaryItemService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 字典值编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] DictionaryItemModel model)
    {
        await _dictionaryItemService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 字典值删除
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] IdInputModel model)
    {
        await _dictionaryItemService.DeleteAsync(model.Id);
        return AppResult.Status200OK("删除成功");
    }
}
