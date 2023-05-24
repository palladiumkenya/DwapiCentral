using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class DefaulterTracingExtract : IDefaulterTracing
    {
        [Key]
        public Guid Id { get; set; }
        public int VisitID { get; set; }
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
        public Guid? PatientId { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
