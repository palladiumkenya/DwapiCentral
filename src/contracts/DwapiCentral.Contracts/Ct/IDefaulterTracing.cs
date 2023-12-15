using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public interface IDefaulterTracing : IExtract
    {
        Guid Id { get; set; }
        int? VisitID { get; set; }
        DateTime? VisitDate { get; set; }
        string? FacilityName { get; set; }
        int? EncounterId { get; set; }
        string? TracingType { get; set; }
        string? TracingOutcome { get; set; }
        int? AttemptNumber { get; set; }
        string? IsFinalTrace { get; set; }
        string? TrueStatus { get; set; }
        string? CauseOfDeath { get; set; }
        string? Comments { get; set; }
        DateTime? BookingDate { get; set; }
       


    }
}
