using MediatR;

namespace DwapiCentral.Hts.Domain.Events;

public class ExtractsUpdatedEvent:INotification
{
    public int SiteCode { get; set; }
    public string Extract { get; set; }
    public int Count { get; set; }
    public string Docket { get; set; } = "HTS";
    public DateTime Date { get; set; } = DateTime.Now;
}