using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
     public interface IPrepCareTermination : IEntity
    {
     
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          string FacilityName { get; set; }
          string PrepNumber { get; set; }
          string HtsNumber { get; set; }
          DateTime? ExitDate { get; set; }
          string ExitReason { get; set; }
          DateTime? DateOfLastPrepDose { get; set; }
       

    }
}
