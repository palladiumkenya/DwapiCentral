using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Manifest;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Manifest : IManifest
    {
        [Key]
        public Guid Id { get; set; }
        public string Docket { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public string Project { get; set; }
        public string UploadMode { get; set; }
        public string DwapiVersion { get; set; }
        public string EmrSetup { get; set; }
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public string EmrVersion { get; set; }
        public Guid Session { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Status { get; set; } = "InProgress";
        public DateTime StatusDate { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? Tag { get; set; }
        public ICollection<Metric> Metrics { get; set; } = new List<Metric>();

        public void SetHandshake()
        {
            End = DateTime.Now;
            UpdateStatus("Queued");
        }

        public void UpdateStatus(string status)
        {
            Status = status;
            StatusDate=DateTime.Now;
        }
    }
}
