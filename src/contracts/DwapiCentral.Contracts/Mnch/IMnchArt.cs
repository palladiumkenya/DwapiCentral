using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
    public  interface IMnchArt : IEntity
    {

          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          string Pkv { get; set; }
          string PatientMnchID { get; set; }
          string PatientHeiID { get; set; }
          string FacilityName { get; set; }
          DateTime? RegistrationAtCCC { get; set; }
          DateTime? StartARTDate { get; set; }
          string StartRegimen { get; set; }
          string StartRegimenLine { get; set; }
          string StatusAtCCC { get; set; }
          DateTime? LastARTDate { get; set; }
          string LastRegimen { get; set; }
          string LastRegimenLine { get; set; }
       

    }
}
