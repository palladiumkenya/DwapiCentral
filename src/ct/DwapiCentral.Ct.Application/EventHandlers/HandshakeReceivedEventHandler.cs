using System.Reflection.Metadata;
using System.Text;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Shared.Domain.Model.Common;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class HandshakeReceivedEventHandler : INotificationHandler<HandshakeReceivedEvent>
{
    private readonly IModel _channel;
    private readonly RabbitOptions _rabbitOptions;

    public HandshakeReceivedEventHandler(IModel channel, RabbitOptions rabbitOptions)
    {
        _channel = channel;
        _rabbitOptions = rabbitOptions;

    }

    public Task Handle(HandshakeReceivedEvent notification, CancellationToken cancellationToken)
    {
        var message = JsonConvert.SerializeObject(notification);
        var body = Encoding.UTF8.GetBytes(message);


        _channel.BasicPublish(_rabbitOptions.ExchangeName, "handshake.route", null, body);

        return Task.CompletedTask;
    }
}

    
    
