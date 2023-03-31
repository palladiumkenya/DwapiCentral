using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
    public  interface IMotherBabyPair : IEntity
    {
        
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          int BabyPatientPK { get; set; }
          int MotherPatientPK { get; set; }
          string BabyPatientMncHeiID { get; set; }
          string MotherPatientMncHeiID { get; set; }
          string PatientIDCCC { get; set; }
          
          

    }
}
