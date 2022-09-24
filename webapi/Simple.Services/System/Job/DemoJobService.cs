using System.Data;
using Quartz;
using Simple.Common.DependencyInjection;
using Simple.Common.Quartz;
using Simple.Common.Quartz.Models;
using static Quartz.Logging.OperationName;

namespace Simple.Services;

/// <summary>
/// 演示环境实现，为了不让用户操作定时任务，给服务器操作不必要的压力
/// </summary>
[AutoInjection(false)]
public class DemoJobService : IJobService
{
    private readonly SimpleDbContext _context;
    private readonly CacheService _cacheService;
    private readonly IQuartzManager _quartzManager;

    public DemoJobService(SimpleDbContext context, CacheService cacheService, IQuartzManager quartzManager)
    {
        _context = context;
        _cacheService = cacheService;
        _quartzManager = quartzManager;
    }

    public async Task<List<JobModel>> GetAsync()
    {
        var jobs = await _context.Set<SysJob>().ToListAsync();
        return MapperHelper.Map<List<JobModel>>(jobs);
    }

    public async Task<PageResultModel<JobModel>> GetPageAsync(JobPageInputModel input)
    {
        var result = new PageResultModel<JobModel>();
        var query = _context.Set<SysJob>().AsQueryable();

        // 根据条件查询
        if (!string.IsNullOrEmpty(input.TimerName))
        {
            query = query.Where(j => EF.Functions.Like(j.Name, $"%{input.TimerName}%"));
        }
        if (input.JobStatus.HasValue)
        {
            bool isEnabled = input.JobStatus.Value == 1;
            query = query.Where(j => j.IsEnabled == isEnabled);
        }

        // 获取总数量
        result.TotalRows = await query.CountAsync();

        // 分页查询
        query = query.OrderBy(j => j.Sort).Page(input.PageNo, input.PageSize);
        var jobs = await query.ToListAsync();
        result.Rows = MapperHelper.Map<List<JobModel>>(jobs);

        result.SetPage(input);
        result.CountTotalPage();

        return result;
    }

    public async Task<int> AddAsync(JobModel model)
    {
        if (await _context.Set<SysJob>().AnyAsync(j => j.Id != model.Id && j.Name == model.TimerName))
        {
            throw AppResultException.Status409Conflict("存在相同任务：任务名称不能重复");
        }

        var jobType = Type.GetType(model.ActionClass);
        if (jobType == null)
        {
            throw AppResultException.Status404NotFound("找不到任务类，添加失败");
        }

        var job = MapperHelper.Map<SysJob>(model);

        // 更新数据库
        await _context.AddAsync(job);
        int ret = await _context.SaveChangesAsync();

        return ret;
    }

    public async Task<int> UpdateAsync(JobModel model)
    {
        if (await _context.Set<SysJob>().AnyAsync(j => j.Id != model.Id && j.Name == model.TimerName))
        {
            throw AppResultException.Status409Conflict("存在相同任务：任务名称不能重复");
        }

        var job = await _context.Set<SysJob>()
            .Where(j => model.Id == j.Id)
            .FirstOrDefaultAsync();

        if (job == null)
        {
            throw AppResultException.Status404NotFound("找不到定时任务，更新失败");
        }

        var jobType = Type.GetType(model.ActionClass);
        if (jobType == null)
        {
            throw AppResultException.Status404NotFound("找不到任务类，更新失败");
        }

        MapperHelper.Map<JobModel, SysJob>(model, job);

        // 更新数据库
        _context.Update(job);
        int ret = await _context.SaveChangesAsync();

        if (ret == 0)
        {
            throw AppResultException.Status200OK("更新记录数为0");
        }

        return ret;
    }

    public async Task<int> DeleteAsync(params Guid[] ids)
    {
        var jobs = await _context.Set<SysJob>()
            .Where(j => ids.Contains(j.Id))
            .ToListAsync();

        // 保存到数据库
        _context.RemoveRange(jobs);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<string>> GetActionClass()
    {
        // 优先从缓存获取
        List<string> result = await _cacheService.GetActionClassAsync();
        if (result.Count > 0)
        {
            return result;
        }

        // 获取所有 IJob 实例
        var actionClasss = AssemblyHelper.GetAssemblies()
            .SelectMany(a => a.GetTypes().Where(t => !t.IsAbstract && t.IsAssignableTo(typeof(IJob))))
            .Select(t => t.FullName!)
            .ToList();
        if (actionClasss != null)
        {
            await _cacheService.SetActionClassAsync(actionClasss);
            return actionClasss;
        }

        // 都获取不到，返回空结果
        return new List<string>();
    }

    public async Task<int> SetEnabledAsync(Guid id, bool isEnabled)
    {
        var job = await _context.Set<SysJob>().Where(j => j.Id == id).FirstOrDefaultAsync();

        if (job == null)
        {
            throw AppResultException.Status404NotFound("找不到定时任务");
        }

        var jobType = Type.GetType(job.ActionClass);
        if (jobType == null)
        {
            throw AppResultException.Status404NotFound("找不到任务类，启动失败");
        }

        // 更新数据库
        job.IsEnabled = isEnabled;
        _context.Update(job);
        int ret = await _context.SaveChangesAsync();

        return ret;
    }

    /// <summary>
    /// 启动所有启用的定时任务。
    /// 在程序启动的时候调用。
    /// </summary>
    /// <returns></returns>
    public async Task StartAll()
    {
        var jobs = await _context.Set<SysJob>().Where(j => j.IsEnabled).ToListAsync();
        foreach (var job in jobs)
        {
            var jobType = Type.GetType(job.ActionClass);
            if (jobType == null)
            {
                continue;
            }

            await AddJob(jobType, job);
        }
    }

    private async Task AddJob(Type jobType, SysJob job)
    {
        var jobInfo = new JobInfo(job.Name);
        jobInfo.Triggers.Add(new TriggerInfo(job.Name, job.Cron));
        await _quartzManager.AddJob(jobType, jobInfo);
    }
}
