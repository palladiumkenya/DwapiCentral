using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
     public interface IHtsPartnerTracing :IExtract
    {
                
          string HtsNumber { get; set; }
          int? PartnerPersonId { get; set; }
          DateTime? TraceDate { get; set; }
          string FacilityName { get; set; }
          string? TraceType { get; set; }
          string? TraceOutcome { get; set; }
          DateTime? BookingDate { get; set; }          
          DateTime? Date_Last_Modified { get; set; }
    }
}
