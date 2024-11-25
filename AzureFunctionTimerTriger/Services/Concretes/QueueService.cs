using Azure.Storage.Queues;
using AzureFunctionTimerTriger.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionTimerTriger.Services.Concretes
{
    public class QueueService : IQueueService
    {
        private readonly QueueClient _queueClient;

        public QueueService(string connStr, string queName)
        {
            _queueClient = new QueueClient(connStr, queName);
            _queueClient.CreateIfNotExists();
        }

        public async Task<string> GetQueue()
        {
            var msgResponse = await _queueClient.ReceiveMessageAsync();
            if(msgResponse.Value != null)
            {
                string msg = msgResponse.Value.Body.ToString();

                await _queueClient.DeleteMessageAsync(msgResponse.Value.MessageId, msgResponse.Value.PopReceipt);

                return msg;
            }
            return "";
        }

        public async Task SetQueue(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                await _queueClient.SendMessageAsync(msg);
            }
        }
    }
}
