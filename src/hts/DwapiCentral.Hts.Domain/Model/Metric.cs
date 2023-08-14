using DwapiCentral.Contracts.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model
{
    public class Metric : IMetric
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid ManifestId { get; set; }
    }
}
