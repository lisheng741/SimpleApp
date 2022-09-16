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
    public async Task<AppResult> List()
    {
        List<DictionaryModel> data = await _dictionaryService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 字典类型查询
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<DictionaryModel> data = await _dictionaryService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    /// <summary>
    /// 字典类型增加
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Add([FromBody] DictionaryModel model)
    {
        await _dictionaryService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    /// <summary>
    /// 字典类型编辑
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Edit([FromBody] DictionaryModel model)
    {
        await _dictionaryService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    /// <summary>
    /// 字典类型删除
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _dictionaryService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    /// <summary>
    /// 字典类型下拉
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> DropDown(string code)
    {
        var items = await _dictionaryService.GetItemsAsync(code);
        return AppResult.Status200OK(data: items);
    }

    /// <summary>
    /// 字典树
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<AppResult> Tree()
    {
        var data = await _dictionaryService.GetTreeAsync();
        return AppResult.Status200OK(data: data);
    }
}
