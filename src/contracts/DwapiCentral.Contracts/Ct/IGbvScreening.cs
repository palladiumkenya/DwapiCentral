using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public  interface IGbvScreening : IExtract
    {
          Guid Id { get; set; }
          int? VisitID { get; set; }
          DateTime VisitDate { get; set; }
          string? FacilityName { get; set; }         
          string? IPV { get; set; }
          string? PhysicalIPV { get; set; }
          string? EmotionalIPV { get; set; }
          string? SexualIPV { get; set; }
          string? IPVRelationship { get; set; }
        
    }
}
