using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class JobService : IJobService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMediator _mediator;

        public JobService(ILogger<JobService> logger, IHttpClientFactory httpClientFactory,IMediator mediator)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _mediator = mediator;
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
        public async Task ProcessDelayedJob()
        {
            _logger.LogWarning($"延迟任务执行：{DateTime.Now}");

            await Task.CompletedTask;
        }

        /// <summary>
        /// 执行循环任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        public async Task ProcessRecurJob<T>(T model) where T : class, new()
        {
            await _mediator.Send(model);
        }
    }
}
