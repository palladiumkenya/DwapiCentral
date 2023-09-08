using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models
{
    public class Manifest : IManifest
    {
        public int SiteCode { get; set; }
        public string Name { get; set; }
        public int Sent { get; set; }
        public int Recieved { get; set; }
        public DateTime DateLogged { get; set; }
        public DateTime DateArrived { get; set; } = DateTime.Now;       
        public DateTime StatusDate { get; set; } = DateTime.Now;     
        public Guid EmrId { get; set; }
        public string EmrName { get; set; }
        public EmrSetup EmrSetup { get; set; }
        public Guid Session { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Tag { get; set; }
        public ManifestStatus ManifestStatus { get; set; }
        public List<Cargo> Cargoes { get; set; } = new List<Cargo>();

        public Guid Id { get; set; }
        public string? Docket { get; set; } = "PREP";
        public string? Project { get; set; }
        public string? DwapiVersion { get; set; }
        public string? EmrVersion { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Status { get; set; } = "InProgress";

        public Manifest()
        {

        }

        public void SetHandshake()
        {
            End = DateTime.Now;
            UpdateStatus("Complete");
        }

        public void UpdateStatus(string status)
        {
            Status = status;
            StatusDate = DateTime.Now;
        }

        public bool IsValid()
        {
            return SiteCode > 0 && Cargoes.Count > 0;
        }
        public void Validate()
        {
            if (!IsValid())
                throw new Exception($"Invalid Manifest,Please ensure the SiteCode [{SiteCode}] is valid and there exists at least one (1) Patient record");
        }
    }
}
