using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Manifest;
    
namespace DwapiCentral.Ct.Domain.Models
{
    public class Metric : IMetric
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid ManifestId { get; set; }
    }
}
