using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Simple.WebApi.Controllers.System;

[Route("api/SysDictType/[action]")]
[ApiController]
public class DictionaryController : ControllerBase
{
    private readonly DictionaryService _dictionaryService;

    public DictionaryController(DictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    [HttpGet]
    public async Task<AppResult> List()
    {
        List<DictionaryModel> data = await _dictionaryService.GetAsync();
        return AppResult.Status200OK(data: data);
    }

    [HttpGet]
    public async Task<AppResult> Page([FromQuery] PageInputModel model)
    {
        PageResultModel<DictionaryModel> data = await _dictionaryService.GetPageAsync(model);
        return AppResult.Status200OK(data: data);
    }

    [HttpPost]
    public async Task<AppResult> Add([FromBody] DictionaryModel model)
    {
        await _dictionaryService.AddAsync(model);
        return AppResult.Status200OK("新增成功");
    }

    [HttpPost]
    public async Task<AppResult> Edit([FromBody] DictionaryModel model)
    {
        await _dictionaryService.UpdateAsync(model);
        return AppResult.Status200OK("更新成功");
    }

    [HttpPost]
    public async Task<AppResult> Delete([FromBody] List<IdInputModel> models)
    {
        await _dictionaryService.DeleteAsync(models.Select(m => m.Id));
        return AppResult.Status200OK("删除成功");
    }

    [HttpGet]
    public async Task<AppResult> DropDown(string code)
    {
        var items = await _dictionaryService.GetItemsAsync(code);
        return AppResult.Status200OK(data: items);
    }

    [HttpGet]
    public async Task<AppResult> Tree()
    {
        var data = await _dictionaryService.GetTreeAsync();
        return AppResult.Status200OK(data: data);
    }
}
