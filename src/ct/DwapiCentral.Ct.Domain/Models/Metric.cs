using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Shared.Domain.Enums;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Metric : IMetric
    {
        [Key]
        public Guid Id { get; set; }        
        public CargoType Type { get; set; } = CargoType.Patient;
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid ManifestId { get; set; }
        public string FacilityName { get; set; }
        public int SiteCode { get; set; }

        public Metric()
        {
        }

        public Metric(string items, Guid facilityManifestId) : this()
        {
            Value = items;
            ManifestId = facilityManifestId;
        }

        public Metric(string items, Guid facilityManifestId, CargoType cargoType,int siteCode, string name) : this(items, facilityManifestId)
        {
            Type = cargoType;
            Name = cargoType.ToString();
            SiteCode= siteCode;
            FacilityName= name;


        }

        public override string ToString()
        {
            return Value;
        }
    }
}
