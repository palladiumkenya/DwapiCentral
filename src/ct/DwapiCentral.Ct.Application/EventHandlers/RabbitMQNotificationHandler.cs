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

        public RabbitMQNotificationHandler(IOptions<RabbitOptions> rabbitMQOptions)
        {
            _rabbitOptions = rabbitMQOptions.Value;

            var factory = new ConnectionFactory
            {
                HostName = _rabbitOptions.HostName,
                Port = _rabbitOptions.Port,
                UserName = _rabbitOptions.UserName,
                Password = _rabbitOptions.Password
            };

            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(_rabbitOptions.ExchangeName, ExchangeType.Fanout);

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
