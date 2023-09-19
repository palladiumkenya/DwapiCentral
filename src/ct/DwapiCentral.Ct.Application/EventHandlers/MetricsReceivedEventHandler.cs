using DwapiCentral.Ct.Application.Events;
using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.EventHandlers
{
    public class MetricsReceivedEventHandler : INotificationHandler<MetricsExtractedEvent>
    {
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public MetricsReceivedEventHandler(IModel channel, RabbitOptions rabbitOptions)
        {
            _channel = channel;
            _rabbitOptions = rabbitOptions;

        }

        public Task Handle(MetricsExtractedEvent notification, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);


            _channel.BasicPublish(_rabbitOptions.ExchangeName, "manifest.route", null, body);

            return Task.CompletedTask;
        }
    }
        
}
