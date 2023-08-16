using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class AdverseEventSourceDto : IPatientAdverse
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string? Severity { get; set; }
        public string? AdverseEventClinicalOutcome { get; set; }
        public string? AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public string? AdverseEventRegimen { get; set; }
        public string? AdverseEventCause { get; set; }
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
