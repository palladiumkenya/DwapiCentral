using DwapiCentral.Ct.Domain.Events;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.EventHandlers;

public class SiteEnrolledEventHandler:INotificationHandler<SiteEnrolledEvent>
{
    public Task Handle(SiteEnrolledEvent notification, CancellationToken cancellationToken)
    {
        Log.Debug(
            $"Publish event NEW site {notification.SiteCode}|{notification.Docket}|{notification.SiteName}");
        return Task.CompletedTask;
    }
}