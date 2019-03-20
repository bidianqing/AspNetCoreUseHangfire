﻿using AspNetCoreUseHangfire.Models;
using Domain.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;

namespace AspNetCoreUseHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        /// <summary>
        /// 创建立刻执行任务
        /// </summary>
        /// <returns></returns>
        [HttpGet("FireAndForgetJobs")]
        public IActionResult CreateFireAndForgetJobs()
        {
            var jobId = BackgroundJob.Enqueue<IJobService>(u => u.ProcessFireAndForgetJobs());

            return new JsonResult(new { success = false, message = $"创建成功，jobId={jobId}" });
        }

        /// <summary>
        /// 创建延迟任务
        /// </summary>
        /// <param name="value"></param>
        [HttpPost("DelayedJobs")]
        public IActionResult CreateDelayedJob([FromBody] DelayedJobModel delayedJobModel)
        {
            string intput = $"{delayedJobModel.Year}/{delayedJobModel.Month}/{delayedJobModel.Day} {delayedJobModel.Hour}:{delayedJobModel.Second}:00 +08:00";

            if (!DateTimeOffset.TryParse(intput, out DateTimeOffset dateTime) || dateTime < DateTimeOffset.Now)
            {
                return new JsonResult(new { success = false, message = "时间有误" });
            }

            // 延迟任务 执行一次
            string jobId = BackgroundJob.Schedule<IJobService>(u => u.ProcessDelayedJob(), dateTime);

            return new JsonResult(new { success = false, message = $"创建成功，jobId={jobId}" });
        }

        /// <summary>
        /// 创建循环任务
        /// </summary>
        /// <returns></returns>
        [HttpPost("RecurJobs")]
        public IActionResult CreateRecurJob()
        {
            string recurringJobId = Guid.NewGuid().ToString();

            // 循环任务 执行多次
            RecurringJob.AddOrUpdate<IJobService>(recurringJobId, (j) => j.ProcessRecurJob(recurringJobId), Cron.Minutely());

            return new JsonResult(new { success = false, message = $"创建成功，jobId={recurringJobId}" });
        }

        /// <summary>
        /// 删除一个Job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpDelete("{jobId}")]
        public IActionResult DeleteJob(string jobId)
        {
            // 没有判断这个Job是属于哪种Job 所以执行了两个删除
            BackgroundJob.Delete(jobId);

            RecurringJob.RemoveIfExists(jobId);

            return new JsonResult(new { success = false, message = $"删除成功" });
        }



        /// <summary>
        /// 立即执行一个Job
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [HttpGet("execute/{jobId}")]
        public IActionResult ExecuteJob(string jobId)
        {
            bool flag = BackgroundJob.Requeue(jobId);
            RecurringJob.Trigger(jobId);


            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            return new JsonResult(new { success = false, message = $"执行成功" });
        }
    }
}
