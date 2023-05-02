using PalladiumDwh.Shared.Interfaces;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IArt 
    {
        DateTime? DOB { get; set; }
        decimal? AgeEnrollment { get; set; }
        decimal? AgeARTStart { get; set; }
        decimal? AgeLastVisit { get; set; }
        DateTime? RegistrationDate { get; set; }
        string PatientSource { get; set; }
        string Gender { get; set; }
        DateTime? StartARTDate { get; set; }
        DateTime? PreviousARTStartDate { get; set; }
        string PreviousARTRegimen { get; set; }
        DateTime? StartARTAtThisFacility { get; set; }
        string StartRegimen { get; set; }
        string StartRegimenLine { get; set; }
        DateTime? LastARTDate { get; set; }
        string LastRegimen { get; set; }
        string LastRegimenLine { get; set; }
        decimal? Duration { get; set; }
        DateTime? ExpectedReturn { get; set; }
        string Provider { get; set; }
        DateTime? LastVisit { get; set; }
        string ExitReason { get; set; }
        DateTime? ExitDate { get; set; }

        string PreviousARTUse { get; set; }
        string PreviousARTPurpose { get; set; }
        DateTime? DateLastUsed { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
