using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IMetric
    {
        Guid Id { get; set; }
        string Type { get; set; } // indicator, facility ,extract
        string Name { get; set; } // indicator, facility ,extract
        string Value { get; set; } // {}
        Guid ManifestId { get; set; }
    }
}
