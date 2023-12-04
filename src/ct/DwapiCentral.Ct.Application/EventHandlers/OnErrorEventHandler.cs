using DwapiCentral.Ct.Domain.Events;
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
    public class OnErrorEventHandler : INotificationHandler<OnErrorEvent>
    {
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public OnErrorEventHandler(IModel channel, RabbitOptions rabbitOptions)
        {
            _channel = channel;
            _rabbitOptions = rabbitOptions;

        }


        public Task Handle(OnErrorEvent notification, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);

            var queueName = "error.queue";


            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);


            _channel.QueueBind(queueName, _rabbitOptions.ExchangeName, "error.route");


            _channel.BasicPublish(_rabbitOptions.ExchangeName, "error.route", null, body);

            return Task.CompletedTask;
        }
    }
}

