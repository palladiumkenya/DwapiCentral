using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class DefaulterTracingSourceDto : IDefaulterTracing
    {
        public Guid Id { get; set; }
        public int? VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public int? EncounterId { get; set; }
        public string? TracingType { get; set; }
        public string? TracingOutcome { get; set; }
        public int? AttemptNumber { get; set; }
        public string? IsFinalTrace { get; set; }
        public string? TrueStatus { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? Comments { get; set; }
        public DateTime? BookingDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? DateCreated { get; set; }
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