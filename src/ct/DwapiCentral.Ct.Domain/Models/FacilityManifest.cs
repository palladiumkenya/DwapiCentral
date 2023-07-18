using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class FacilityManifest
    {
        public Guid Id { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public string? EmrVersion { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public List<int> PatientPKs { get; set; } = new List<int>();
        public string? Metrics { get; set; }
        public List<FacMetric> FacMetrics { get; set; } = new List<FacMetric>();
        public int PatientCount => PatientPKs.Count;
        public UploadMode UploadMode { get; set; }
        public string? DwapiVersion { get; set; }
        public string? Docket { get; set; } = "CT";

        public Guid Session { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Tag { get; set; }
        public string Items => string.Join(",", PatientPKs);
    }
}
