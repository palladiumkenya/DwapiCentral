using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IPharmacy : IExtract
    {
          Guid Id { get; set; }
          int? VisitID { get; set; }
          string? Drug { get; set; }
          string? Provider { get; set; }
          DateTime? DispenseDate { get; set; }
          decimal? Duration { get; set; }
          DateTime? ExpectedReturn { get; set; }
          string? TreatmentType { get; set; }
          string? RegimenLine { get; set; }
          string? PeriodTaken { get; set; }
          string? ProphylaxisType { get; set; }         
          string? RegimenChangedSwitched { get; set; }
          string? RegimenChangeSwitchReason { get; set; }
          string? StopRegimenReason { get; set; }
          DateTime? StopRegimenDate { get; set; }
    }
}