using System.Reflection.Metadata;
using DwapiCentral.Ct.Domain.Events;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class HandshakeReceivedEventHandler:INotificationHandler<HandshakeReceivedEvent>
{
    public Task Handle(HandshakeReceivedEvent notification, CancellationToken cancellationToken)
    {
        Log.Debug(
            $"Publish event SPOT {notification.SiteCode}");
        return Task.CompletedTask;
    }
}