using DwapiCentral.Ct.Domain.Events;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class ManifestStatusUpdatedEventHandler:INotificationHandler<ManifestStatusUpdatedEvent>
{
    public Task Handle(ManifestStatusUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Log.Debug(
            $"Publish event SPOT {notification.Status}");
        return Task.CompletedTask;
    }
}