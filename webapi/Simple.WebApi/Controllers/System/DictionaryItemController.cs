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
    public async Task<List<DictionaryItemModel>> List()
    {
        List<DictionaryItemModel> data = await _dictionaryItemService.GetAsync();
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 字典值查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<DictionaryItemModel>> Page([FromQuery] DictionaryItemPageInputModel model)
    {
        PageResultModel<DictionaryItemModel> data = await _dictionaryItemService.GetPageAsync(model);
        return ResultHelper.Result200OK(data: data);
    }

    /// <summary>
    /// 字典值增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] DictionaryItemModel model)
    {
        var data = await _dictionaryItemService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 字典值编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] DictionaryItemModel model)
    {
        var data = await _dictionaryItemService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 字典值删除
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] IdInputModel model)
    {
        var data = await _dictionaryItemService.DeleteAsync(model.Id);
        return ResultHelper.Result200OK("删除成功", data);
    }
}
