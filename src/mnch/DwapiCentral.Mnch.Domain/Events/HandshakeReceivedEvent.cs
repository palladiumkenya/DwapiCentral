using MediatR;

namespace DwapiCentral.Mnch.Domain.Events;

public class HandshakeReceivedEvent:INotification
{
    public string Name { get; set; }
    public Guid ManifestId { get; set; }
    public int SiteCode { get; set; }
    public string Docket { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Status { get; set; }
    

    
}