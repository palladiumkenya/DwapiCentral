using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IStatus
    {
        string ExitDescription { get; set; }
        DateTime? ExitDate { get; set; }
        string ExitReason { get; set; }
        string TOVerified { get; set; }
        DateTime? TOVerifiedDate { get; set; }
        DateTime? ReEnrollmentDate { get; set; }

        string ReasonForDeath { get; set; }
        string SpecificDeathReason { get; set; }
        DateTime? DeathDate { get; set; }
        DateTime? EffectiveDiscontinuationDate { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
