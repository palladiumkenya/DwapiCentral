using DwapiCentral.Hts.Application.Events;
using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.EventHandlers
{
    public class MetricsEventHandler : INotificationHandler<HtsMetricsEvent>
    {
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public MetricsEventHandler(IModel channel, RabbitOptions rabbitOptions)
        {
            _channel = channel;
            _rabbitOptions = rabbitOptions;

        }



        public Task Handle(HtsMetricsEvent notification, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);


            _channel.BasicPublish(_rabbitOptions.ExchangeName, "indicator.route", null, body);

            return Task.CompletedTask;
        }
    }
}
