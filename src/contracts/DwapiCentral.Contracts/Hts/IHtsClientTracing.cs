using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public interface IHtsClientTracing : IExtract
    {
        string HtsNumber { get; set; }
        string FacilityName { get; set; }
        String? TracingType { get; set; }
        DateTime? TracingDate { get; set; }
        string? TracingOutcome { get; set; }       
        DateTime? Date_Last_Modified { get; set; }
    }
}
