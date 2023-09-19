using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;

namespace DwapiCentral.Hts.Domain.Events;

public class ManifestReceivedEvent : INotification
{
    public Guid ManifestId { get; set; }
    public int SiteCode { get; set; }
    public string Docket { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public UploadMode UploadMode { get; set; }    
    public EmrSetup EmrSetup { get; set; }
    public string EmrVersion { get; set; }
    public string DwapiVersion { get; set; }
    public string Status { get; set; }
    public ICollection<Cargo> Metrics { get; set; }

}