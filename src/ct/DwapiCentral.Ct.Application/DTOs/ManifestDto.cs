using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class ManifestDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Docket { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime BuildDate { get; set; }
        public int PatientCount { get; set; }
        public string Cargo { get; set; }
        public Guid? Session { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string Tag { get; set; }

        public UploadMode UploadMode { get; set; }
        public string? DwapiVersion { get; set; }
        
        public string EmrName { get; set; }
        public string? EmrVersion { get; set; }
        public EmrSetup EmrSetup { get; set; }

        public List<Metric> Metrics { get; set; } = new List<Metric>();



        public ManifestDto(Manifest manifest,FacilityManifest facilityManifest)
        {
            Id = manifest.Id;
            FacilityCode = manifest.SiteCode;
            FacilityName = manifest.Name;
            Docket = "CT";
            LogDate = manifest.Created;
            BuildDate = manifest.Created;
            PatientCount = facilityManifest.PatientCount;
            Cargo = facilityManifest.Metrics;
            Session = manifest.Session;
            Start = manifest.Start;
            End = manifest.End;
            Tag = manifest.Tag;
            UploadMode = manifest.UploadMode;
            DwapiVersion= manifest.DwapiVersion;
            EmrName = manifest.EmrName;
            EmrVersion = manifest.EmrVersion;
            EmrSetup= manifest.EmrSetup;
            Metrics = manifest.Metrics.Where(x => x.Type != CargoType.Patient).ToList();
        }

     
    }
}
