using Microsoft.Extensions.Logging;
using System;

namespace Domain.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger _logger;

        public JobService(ILogger<JobService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 立即执行任务
        /// </summary>
        public void ProcessFireAndForgetJobs()
        {
            _logger.LogWarning("Fire-and-forget!");
        }

        /// <summary>
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        public void ProcessDelayedJob()
        {
            _logger.LogWarning($"{DateTime.Now}");
        }

        /// <summary>
        /// 执行循环任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        public void ProcessRecurJob(string recurringJobId)
        {
            _logger.LogWarning($"[{recurringJobId}] {DateTime.Now}");
        }
    }
}
