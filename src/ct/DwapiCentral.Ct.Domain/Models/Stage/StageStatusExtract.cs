using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageStatusExtract : StageExtract, IStatus
    {
        public string? ExitDescription { get ; set ; }
        public DateTime ExitDate { get ; set ; }
        public string? ExitReason { get ; set ; }
        public string? TOVerified { get ; set ; }
        public DateTime? TOVerifiedDate { get ; set ; }
        public DateTime? ReEnrollmentDate { get ; set ; }
        public string? ReasonForDeath { get ; set ; }
        public string? SpecificDeathReason { get ; set ; }
        public DateTime? DeathDate { get ; set ; }
        public DateTime? EffectiveDiscontinuationDate { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } 
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
