using MediatR;

namespace DwapiCentral.Mnch.Domain.Events;

public class ExtractsUpdatedEvent:INotification
{
    public int SiteCode { get; set; }
    public string Extract { get; set; }
    public int Count { get; set; }
    public string Docket { get; set; } = "MNCH";
    public DateTime Date { get; set; } = DateTime.Now;
}