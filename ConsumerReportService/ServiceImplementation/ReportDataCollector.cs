using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQManages.Subscriber;

namespace ConsumerReportService.ServiceImplementation
{
    public class ReportDataCollector : IHostedService
    {
        private readonly ISubscriber _subscriber;
        private readonly IMemoryReportStorage _memoryReportStorage;

        public ReportDataCollector(ISubscriber subscriber, IMemoryReportStorage memoryReportStorage)
        {
            _subscriber = subscriber;
            _memoryReportStorage = memoryReportStorage;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(ProcessMessage);
            return Task.CompletedTask;
        }

        private bool ProcessMessage(string message, IDictionary<string, object> headers)
        {
            if (message.Contains("Producer"))
            {
                var product = JsonConvert.DeserializeObject<Product>(message);
                
                _memoryReportStorage.Add(new Report
                {
                    ProductName = product.Messages
                });
            }

            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
