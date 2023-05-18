using DwapiCentral.Ct.Domain.Events;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class ExtractsUpdatedEventHandler:INotificationHandler<ExtractsUpdatedEvent>
{
    public Task Handle(ExtractsUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Log.Debug(
            $"Publish event SPOT stats {notification.SiteCode}|{notification.Docket}|{notification.Extract}-{notification.Count}|");
        return Task.CompletedTask;
    }
}