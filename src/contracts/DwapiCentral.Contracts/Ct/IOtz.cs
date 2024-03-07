using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IOtz : IExtract
    {
          Guid Id { get; set; }
          ulong Mhash { get; set; }
          string? FacilityName { get; set; }
          int? VisitID { get; set; }
          DateTime? VisitDate { get; set; }
          DateTime? OTZEnrollmentDate { get; set; }
          string? TransferInStatus { get; set; }
          string? ModulesPreviouslyCovered { get; set; }
          string? ModulesCompletedToday { get; set; }
          string? SupportGroupInvolvement { get; set; }
          string? Remarks { get; set; }
          string? TransitionAttritionReason { get; set; }
          DateTime? OutcomeDate { get; set; }
         
    }
}
