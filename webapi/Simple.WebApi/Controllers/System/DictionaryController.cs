using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

/// <summary>
/// 字典管理
/// </summary>
[Route("api/SysDictType/[action]")]
[ApiController]
[Authorize]
public class DictionaryController : ControllerBase
{
    private readonly DictionaryService _dictionaryService;

    public DictionaryController(DictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    /// <summary>
    /// 字典类型列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<DictionaryModel>> List()
    {
        return await _dictionaryService.GetAsync();
    }

    /// <summary>
    /// 字典类型查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResultModel<DictionaryModel>> Page([FromQuery] PageInputModel model)
    {
        return await _dictionaryService.GetPageAsync(model);
    }

    /// <summary>
    /// 字典类型增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Add([FromBody] DictionaryModel model)
    {
        var data = await _dictionaryService.AddAsync(model);
        return ResultHelper.Result200OK("新增成功", data);
    }

    /// <summary>
    /// 字典类型编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Edit([FromBody] DictionaryModel model)
    {
        var data = await _dictionaryService.UpdateAsync(model);
        return ResultHelper.Result200OK("更新成功", data);
    }

    /// <summary>
    /// 字典类型删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<int> Delete([FromBody] List<IdInputModel> models)
    {
        var data = await _dictionaryService.DeleteAsync(models.Select(m => m.Id));
        return ResultHelper.Result200OK("删除成功", data);
    }

    /// <summary>
    /// 字典类型下拉
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<DictionaryItemModel>> DropDown(string code)
    {
        var items = await _dictionaryService.GetItemsAsync(code);
        return items;
    }

    /// <summary>
    /// 字典树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<DictionaryTreeModel>> Tree()
    {
        return await _dictionaryService.GetTreeAsync();
    }
}
