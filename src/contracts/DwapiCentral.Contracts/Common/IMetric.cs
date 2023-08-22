using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Common
{
    public interface IMetric
    {
        Guid Id { get; set; }      
       
        Guid ManifestId { get; set; }
    }
}
