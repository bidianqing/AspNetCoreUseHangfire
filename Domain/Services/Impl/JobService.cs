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
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        public void ProcessDelayedJob()
        {
            _logger.LogWarning($"{DateTime.Now}");
        }

        public void Process(string jobId)
        {
            _logger.LogWarning($"[{jobId}] {DateTime.Now}");
        }

        public void ProcessRecurJob(string recurringJobId)
        {
            _logger.LogWarning($"[{recurringJobId}] {DateTime.Now}");
        }
    }
}
