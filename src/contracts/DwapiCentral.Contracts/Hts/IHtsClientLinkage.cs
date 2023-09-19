using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public  interface IHtsClientLinkage : IExtract
    {
            string FacilityName { get; set; }            
            string HtsNumber { get; set; }   
            string? EnrolledFacilityName { get; set; }
            DateTime? ReferralDate { get; set; }
            DateTime? DateEnrolled { get; set; }
            DateTime? DatePrefferedToBeEnrolled { get; set; }
            string? FacilityReferredTo { get; set; }
            string? HandedOverTo { get; set; }
            string? HandedOverToCadre { get; set; }
            string? ReportedCCCNumber { get; set; }
            DateTime? ReportedStartARTDate { get; set; }

       
    }
}
