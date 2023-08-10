using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public interface IArt : IExtract
    {
        Guid Id { get; set; }
        DateTime LastARTDate { get; set; }
        DateTime? LastVisit { get; set; }
        DateTime? DOB { get; set; }
        decimal? AgeEnrollment { get; set; }
        decimal? AgeARTStart { get; set; }
        decimal? AgeLastVisit { get; set; }
        DateTime? RegistrationDate { get; set; }
        string? Gender { get; set; }
        string? PatientSource { get; set; }
        DateTime? StartARTDate { get; set; }
        DateTime? PreviousARTStartDate { get; set; }
        string? PreviousARTRegimen { get; set; }
        DateTime? StartARTAtThisFacility { get; set; }
        string? StartRegimen { get; set; }
        string? StartRegimenLine { get; set; }        
        string? LastRegimen { get; set; }
        string? LastRegimenLine { get; set; }
        decimal? Duration { get; set; }
        DateTime? ExpectedReturn { get; set; }
        string? Provider { get; set; }        
        string? ExitReason { get; set; }
        DateTime? ExitDate { get; set; }        
        string? PreviousARTUse { get; set; }
        string? PreviousARTPurpose { get; set; }
        DateTime? DateLastUsed { get; set; }    

               
        
    }
}
