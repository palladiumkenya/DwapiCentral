using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IOvc : IExtract
    {
          Guid Id { get; set; }          
          int? VisitID { get; set; }
          DateTime? VisitDate { get; set; }
          string? FacilityName { get; set; }
          DateTime? OVCEnrollmentDate { get; set; }
          string? RelationshipToClient { get; set; }
          string? EnrolledinCPIMS { get; set; }
          string? CPIMSUniqueIdentifier { get; set; }
          string? PartnerOfferingOVCServices { get; set; }
          string? OVCExitReason { get; set; }
          DateTime? ExitDate { get; set; }
        
    }
}
