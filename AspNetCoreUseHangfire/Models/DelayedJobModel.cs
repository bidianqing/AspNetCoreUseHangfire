using System;

namespace AspNetCoreUseHangfire.Models
{
    /// <summary>
    /// 延迟任务
    /// </summary>
    public class DelayedJobModel
    {
        public DateTime TriggerTime { get; set; }
    }
}
