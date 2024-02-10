using DwapiCentral.Contracts.Common;
using DwapiCentral.Shared.Domain.Entities;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model
{
    public class Cargo : IMetric
    {
        public CargoType Type { get; set; }
        public string Items { get; set; }
        public Guid ManifestId { get; set; }
        public int SiteCode { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        //contracts
        public Guid Id { get; set; }
        
        public Cargo()
        {
        }
    }
}
