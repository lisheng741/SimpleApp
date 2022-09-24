namespace Simple.Services;

public interface IJobService
{
    Task<List<JobModel>> GetAsync();

    Task<PageResultModel<JobModel>> GetPageAsync(JobPageInputModel input);

    Task<int> AddAsync(JobModel model);

    Task<int> UpdateAsync(JobModel model);

    Task<int> DeleteAsync(params Guid[] ids);

    Task<List<string>> GetActionClass();

    Task<int> SetEnabledAsync(Guid id, bool isEnabled);

    Task StartAll();
}
