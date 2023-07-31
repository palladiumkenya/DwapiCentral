using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public  interface IHtsClientLinkage : IEntity
    {
            string FacilityName { get; set; }            
            string HtsNumber { get; set; }            
            bool? Processed { get; set; }
            string QueueId { get; set; }
            string Status { get; set; }
            DateTime? StatusDate { get; set; }
            DateTime? DateExtracted { get; set; }
            DateTime? PhoneTracingDate { get; set; }
            DateTime? PhysicalTracingDate { get; set; }
            string TracingOutcome { get; set; }
            string CccNumber { get; set; }
            string EnrolledFacilityName { get; set; }
            DateTime? ReferralDate { get; set; }
            DateTime? DateEnrolled { get; set; }
            DateTime? DatePrefferedToBeEnrolled { get; set; }
            string FacilityReferredTo { get; set; }
            string HandedOverTo { get; set; }
            string HandedOverToCadre { get; set; }
            string ReportedCCCNumber { get; set; }
            DateTime? ReportedStartARTDate { get; set; }
            Guid FacilityId { get; set; }
    }
}
