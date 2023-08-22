using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class CervicalCancerScreeningSourceDto :  ICervicalCancerScreening
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }        
        public string? VisitType { get; set; }
        public string? ScreeningMethod { get; set; }
        public string? TreatmentToday { get; set; }
        public string? ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string? ScreeningType { get; set; }
        public string? ScreeningResult { get; set; }
        public string? PostTreatmentComplicationCause { get; set; }
        public string? OtherPostTreatmentComplication { get; set; }
        public string? ReferralReason { get; set; }        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
