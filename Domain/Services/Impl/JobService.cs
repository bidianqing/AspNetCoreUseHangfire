﻿using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public JobService(ILogger<JobService> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 立即执行任务
        /// </summary>
        public async Task ProcessFireAndForgetJobs()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("https://www.baidu.com");

            string content = await httpResponseMessage.Content.ReadAsStringAsync();
            _logger.LogWarning(content);
            _logger.LogWarning("Fire-and-forget!");
        }

        /// <summary>
        /// 执行延迟任务
        /// </summary>
        /// <returns></returns>
        public void ProcessDelayedJob()
        {
            _logger.LogWarning($"延迟任务执行：{DateTime.Now}");
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
