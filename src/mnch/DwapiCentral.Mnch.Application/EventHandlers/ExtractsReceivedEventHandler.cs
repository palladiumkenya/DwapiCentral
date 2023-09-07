using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Application.EventHandlers
{
    public class ExtractsReceivedEventHandler : INotificationHandler<ExtractsReceivedEvent>
    {
        private readonly IModel _channel;
        private readonly RabbitOptions _rabbitOptions;

        public ExtractsReceivedEventHandler(IModel channel, RabbitOptions rabbitOptions)
        {
            _channel = channel;
            _rabbitOptions = rabbitOptions;

        }


        public Task Handle(ExtractsReceivedEvent notification, CancellationToken cancellationToken)
        {
            var message = JsonConvert.SerializeObject(notification);
            var body = Encoding.UTF8.GetBytes(message);


            _channel.BasicPublish(_rabbitOptions.ExchangeName, "extracts.route", null, body);

            return Task.CompletedTask;
        }
    }
}
