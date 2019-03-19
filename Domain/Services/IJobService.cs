using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IJobService
    {
        void Process(string jobId);

        /// <summary>
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        void ProcessDelayedJob();

        void ProcessRecurJob(string recurringJobId);
    }
}
