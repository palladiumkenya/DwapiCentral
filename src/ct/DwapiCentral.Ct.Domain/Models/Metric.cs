using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Metric : IMetric
    {
        public CargoType CargoType { get; set; }
        
        public Guid ManifestId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public Metric()
        {

        }

        public Metric(CargoType cargoType, string metric)
        {
            CargoType = cargoType;
            
        }
    }
}
