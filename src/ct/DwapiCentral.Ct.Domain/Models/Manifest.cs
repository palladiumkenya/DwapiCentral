using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Shared.Domain.Enums;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Manifest : IManifest
    {
        public Guid Id { get; set; }
        public string? Docket { get; set; }
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public string? Project { get; set; }
        public UploadMode UploadMode { get; set; }
        public string? DwapiVersion { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public string? EmrVersion { get; set; }
        public Guid Session { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Status { get; set; } = "InProgress";
        public DateTime StatusDate { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
        public string? Tag { get; set; }

        
       
        public ICollection<Metric> Metrics { get; set; } = new List<Metric>();

        public Manifest()
        {
        }
        public Manifest(int siteCode, int patientCount, Guid? id) : this()
        {
            SiteCode = siteCode;            
            Created = DateTime.Now;
            if (null != id)
            {
                Id =   id.Value;
            }
            else
            {
                Id = Guid.NewGuid();
            }
        }


        public void AddCargo(string items)
        {
            var cargo = new Metric(items, Id, CargoType.Patient, SiteCode, Name);
            Metrics.Add(cargo);
        }

        public void AddMetricsCargo(string items)
        {
            var cargo = new Metric(items, Id);
            Metrics.Add(cargo);
            
        }

        public void AddMetricsCargo(FacMetric metric)
        {
            var cargo = new Metric(metric.Metric, Id, metric.CargoType, SiteCode, Name);
            Metrics.Add(cargo);
        }

        public static Manifest Create(FacilityManifest manifest)
        {
            var fm = new Manifest(manifest.SiteCode, manifest.PatientCount, manifest.Id);

            fm.EmrId = manifest.EmrId;
            fm.EmrName = manifest.EmrName;
            fm.EmrSetup = manifest.EmrSetup;
            fm.Name = manifest.Name;
            fm.UploadMode = manifest.UploadMode;
            fm.Session = manifest.Session;
            fm.Start = manifest.Start;
            fm.Tag = manifest.Tag;
            fm.EmrVersion= manifest.EmrVersion;
            fm.DwapiVersion = manifest.DwapiVersion;
            fm.Docket = manifest.Docket;

            fm.AddCargo(manifest.Items);

            if (manifest.FacMetrics.Any())
            {
                foreach (var m in manifest.FacMetrics)
                {
                    fm.AddMetricsCargo(m);
                }
            }
            return fm;
        }
       

        public void EndSession(DateTime end)
        {
            End = end;
        }
        public void SetHandshake()
        {
            End = DateTime.Now;
            UpdateStatus("Queued");
        }

        public void UpdateStatus(string status)
        {
            Status = status;
            StatusDate = DateTime.Now;
        }

    }
}
