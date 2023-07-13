using System;
using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class PatientPharmacyExtract : IPharmacy
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? VisitID { get; set; }
        public DateTime DispenseDate { get; set; }
        public string? Drug { get; set; }
        public string? Provider { get; set; }       
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string? TreatmentType { get; set; }
        public string? RegimenLine { get; set; }
        public string? PeriodTaken { get; set; }
        public string? ProphylaxisType { get; set; }        
        public string? RegimenChangedSwitched { get; set; }
        public string? RegimenChangeSwitchReason { get; set; }
        public string? StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
