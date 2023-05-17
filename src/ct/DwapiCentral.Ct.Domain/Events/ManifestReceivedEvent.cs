using MediatR;

namespace DwapiCentral.Ct.Domain.Events;

public class ManifestReceivedEvent : INotification
{
    public Guid ManifestId { get; set; }
    public int SiteCode { get; set; }
    public string Docket { get; set; } = "CT";
    public DateTime Date { get; set; } = DateTime.Now;

    public ManifestReceivedEvent(Guid manifestId, int siteCode)
    {
        ManifestId = manifestId;
        SiteCode = siteCode;
    }
}