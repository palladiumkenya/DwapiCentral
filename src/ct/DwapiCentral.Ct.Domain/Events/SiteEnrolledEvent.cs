using MediatR;

namespace DwapiCentral.Ct.Domain.Events;

public class SiteEnrolledEvent:INotification
{
    public int SiteCode { get; set; }
    public string SiteName { get; set; }
    public string Docket { get; set; } = "CT";
    public DateTime Date { get; set; } = DateTime.Now;

    public SiteEnrolledEvent(int siteCode, string siteName)
    {
        SiteCode = siteCode;
        SiteName = siteName;
    }
}