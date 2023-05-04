using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IMetric
    {
        Guid ManifestId { get; set; }
        string Name { get; set; }
        string Value { get; set; }
        string Type { get; set; }
    }
}
