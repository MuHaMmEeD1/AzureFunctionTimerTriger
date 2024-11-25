using System;
using AzureFunctionTimerTriger.Services.Abstracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionTimerTriger
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly IQueueService _queueService;


        public Function1(ILoggerFactory loggerFactory, IQueueService queueService)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _queueService = queueService;
        }

        [Function("WriteMessage")]
        public async Task WriteMessage([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer)
        {
            await _queueService.SetQueue("Test 1 OK");
            _logger.LogInformation($"WriteMessage OK");
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }

        [Function("ReadMessage")]
        public async Task ReadMessage([TimerTrigger("*/7 * * * * *")] TimerInfo myTimer)
        {
            string msg = await _queueService.GetQueue();
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
