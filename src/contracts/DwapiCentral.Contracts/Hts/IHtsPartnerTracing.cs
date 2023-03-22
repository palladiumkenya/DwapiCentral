using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
     public interface IHtsPartnerTracing :IEntity
    {
          string FacilityName { get; set; }
          int SiteCode { get; set; }
          int PatientPk { get; set; }
          string HtsNumber { get; set; }
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          string TraceType { get; set; }
          DateTime? TraceDate { get; set; }
          string TraceOutcome { get; set; }
          DateTime? BookingDate { get; set; }
          Guid FacilityId { get; set; }
          int? PartnerPersonID { get; set; }
    }
}
