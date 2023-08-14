using DwapiCentral.Contracts.Hts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model.Stage
{
    public class StageHtsClientLinkage : IHtsClientLinkage
    {
        public string FacilityName { get; set; }
        public string HtsNumber { get; set; }
        public bool? Processed { get; set; }
        public string QueueId { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? PhoneTracingDate { get; set; }
        public DateTime? PhysicalTracingDate { get; set; }
        public string TracingOutcome { get; set; }
        public string CccNumber { get; set; }
        public string EnrolledFacilityName { get; set; }
        public DateTime? ReferralDate { get; set; }
        public DateTime? DateEnrolled { get; set; }
        public DateTime? DatePrefferedToBeEnrolled { get; set; }
        public string FacilityReferredTo { get; set; }
        public string HandedOverTo { get; set; }
        public string HandedOverToCadre { get; set; }
        public string ReportedCCCNumber { get; set; }
        public DateTime? ReportedStartARTDate { get; set; }
        public Guid FacilityId { get; set; }
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public bool Voided { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }
    }
}
