using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
     public interface IPrepPharmacy : IEntity
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
          int? VisitID { get; set; }
          string RegimenPrescribed { get; set; }
          DateTime? DispenseDate { get; set; }
          decimal? Duration { get; set; }
        

    }
}
