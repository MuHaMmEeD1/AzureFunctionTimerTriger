using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunctionTimerTriger.Services.Abstracts
{
    public interface IQueueService
    {
        Task SetQueue(string msg);
        Task<string> GetQueue();


    }
}
