using DwapiCentral.Contracts.Hts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model
{
    public class HtsClientLinkage : IHtsClientLinkage
    {
        [Key]
        public Guid Id { get; set; }
        public string? RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string HtsNumber { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public string FacilityName { get; set; }        
        public string? EnrolledFacilityName { get; set; }
        public DateTime? ReferralDate { get; set; }        
        public DateTime? DatePrefferedToBeEnrolled { get; set; }
        public string? FacilityReferredTo { get; set; }
        public string? HandedOverTo { get; set; }
        public string? HandedOverToCadre { get; set; }
        public string? ReportedCCCNumber { get; set; }
        public DateTime? ReportedStartARTDate { get; set; }
       
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
