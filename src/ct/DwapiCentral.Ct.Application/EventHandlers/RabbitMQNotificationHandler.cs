using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.EventHandlers
{
    public class RabbitMQNotificationHandler<T> : INotificationHandler<T> where T : INotification
    {
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public RabbitMQNotificationHandler(IModel channel, RabbitOptions rabbitOptions)
        {
            _channel= channel;
            _rabbitOptions = rabbitOptions;

        }

        public Task Handle(T notification, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(_rabbitOptions.ExchangeName, "", null, body);

            return Task.CompletedTask;
        }
    }
}
