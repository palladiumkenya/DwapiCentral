using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public  interface IStatus : IExtract
    {

          Guid Id { get; set; }
          string? ExitDescription { get; set; }
          DateTime? ExitDate { get; set; }
          string? ExitReason { get; set; }                 
          string? TOVerified { get; set; }
          DateTime? TOVerifiedDate { get; set; }
          DateTime? ReEnrollmentDate { get; set; }
          string? ReasonForDeath { get; set; }
          string? SpecificDeathReason { get; set; }
          DateTime? DeathDate { get; set; }
          DateTime? EffectiveDiscontinuationDate { get; set; }
    }
}
