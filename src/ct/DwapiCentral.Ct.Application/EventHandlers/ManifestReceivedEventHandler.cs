using DwapiCentral.Ct.Domain.Events;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class ManifestReceivedEventHandler:INotificationHandler<ManifestReceivedEvent>
{
    public Task Handle(ManifestReceivedEvent notification, CancellationToken cancellationToken)
    {
        Log.Debug(
            $"Publish event SPOT {notification.SiteCode}");
        return Task.CompletedTask;
    }
}