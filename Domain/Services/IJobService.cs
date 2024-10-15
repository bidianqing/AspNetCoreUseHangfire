using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IJobService
    {
        /// <summary>
        /// 立即执行任务
        /// </summary>
        Task ProcessFireAndForgetJobs();

        /// <summary>
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        Task ProcessDelayedJob();

        /// <summary>
        /// 执行循环任务
        /// </summary>
        /// <param name="model"></param>
        Task ProcessRecurJob<T>(T model) where T : class, new();
    }
}
