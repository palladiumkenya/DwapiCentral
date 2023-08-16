using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class OtzSourceDto : IOtz
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public string? FacilityName { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string? TransferInStatus { get; set; }
        public string? ModulesPreviouslyCovered { get; set; }
        public string? ModulesCompletedToday { get; set; }
        public string? SupportGroupInvolvement { get; set; }
        public string? Remarks { get; set; }
        public string? TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
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