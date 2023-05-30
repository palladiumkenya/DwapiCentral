using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class PatientArtSourceDto : IArt
    {
        public Guid Id { get ; set ; }
        public DateTime LastARTDate { get; set; }        
        public DateTime? DOB { get ; set ; }
        public decimal? AgeEnrollment { get ; set ; }
        public decimal? AgeARTStart { get ; set ; }
        public decimal? AgeLastVisit { get ; set ; }
        public DateTime? RegistrationDate { get ; set ; }
        public string? Gender { get ; set ; }
        public string? PatientSource { get ; set ; }
        public DateTime? StartARTDate { get ; set ; }
        public DateTime? PreviousARTStartDate { get ; set ; }
        public string? PreviousARTRegimen { get ; set ; }
        public DateTime? StartARTAtThisFacility { get ; set ; }
        public string? StartRegimen { get ; set ; }
        public string? StartRegimenLine { get ; set ; }
        public DateTime? LastVisit { get; set; }
        public string? LastRegimen { get ; set ; }
        public string? LastRegimenLine { get ; set ; }
        public decimal? Duration { get ; set ; }
        public DateTime? ExpectedReturn { get ; set ; }
        public string? Provider { get ; set ; }
        public string? ExitReason { get ; set ; }
        public DateTime? ExitDate { get ; set ; }
        public string? PreviousARTUse { get ; set ; }
        public string? PreviousARTPurpose { get ; set ; }
        public DateTime? DateLastUsed { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? DateCreated { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
