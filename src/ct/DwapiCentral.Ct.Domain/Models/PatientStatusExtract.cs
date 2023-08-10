using System;
using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class PatientStatusExtract : IStatus
    {
        [Key]
        public Guid Id { get ; set ; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime ExitDate { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public string? ExitDescription { get ; set ; }        
        public string? ExitReason { get ; set ; }       
        public string? TOVerified { get ; set ; }        
        public DateTime? ReEnrollmentDate { get ; set ; }
        public string? ReasonForDeath { get ; set ; }
        public string? SpecificDeathReason { get ; set ; }
        public DateTime? DeathDate { get ; set ; }
        public DateTime? EffectiveDiscontinuationDate { get ; set ; }      
        public DateTime? Date_Created { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
