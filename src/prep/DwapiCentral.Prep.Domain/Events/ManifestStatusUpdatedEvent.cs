using MediatR;

namespace DwapiCentral.Prep.Domain.Events;

public class ManifestStatusUpdatedEvent:INotification
{
    public Guid ManifestId { get; set; }
    public string Status { get; set; }
    public string Docket { get; set; } = "PREP";
    public DateTime Date { get; set; } = DateTime.Now;

    public ManifestStatusUpdatedEvent(Guid manifestId, string status)
    {
        ManifestId = manifestId;
        Status = status;
    }
}