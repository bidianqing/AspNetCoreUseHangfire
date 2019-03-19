namespace AspNetCoreUseHangfire.Models
{
    /// <summary>
    /// 延迟任务
    /// </summary>
    public class DelayedJobModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Second { get; set; }
    }
}
