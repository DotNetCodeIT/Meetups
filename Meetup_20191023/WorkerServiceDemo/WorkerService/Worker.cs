using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        
        public Worker(ILogger<Worker> logger,IConfiguration configuration)
        {
            _logger = logger;
            queueConnectionString = configuration["Queue:ConnectionString"];
        }

        private string queueConnectionString = "";


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(queueConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("demoqueue");
            IEnumerable<CloudQueueMessage> messages;

            while (!stoppingToken.IsCancellationRequested)
            {
                messages = await queue.GetMessagesAsync(10);
                if (messages.Count() > 0)
                {
                    foreach (var item in messages)
                    {
                        _logger.LogInformation("Queue message: {time} --> {message}", item.InsertionTime, item.AsString);
                        await queue.DeleteMessageAsync(item);
                    }

                }
                else
                {
                    _logger.LogInformation("Worker waiting at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);
                }



            }
        }
    }
}
