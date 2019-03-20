namespace Domain.Services
{
    public interface IJobService
    {
        /// <summary>
        /// 立即执行任务
        /// </summary>
        void ProcessFireAndForgetJobs();

        /// <summary>
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        void ProcessDelayedJob();

        /// <summary>
        /// 执行循环任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        void ProcessRecurJob(string recurringJobId);
    }
}
