using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public interface IJobService
    {
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
