using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
     public interface IHei : IEntity
    {
        
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          string FacilityName { get; set; }
          string PatientMnchID { get; set; }
          DateTime? DNAPCR1Date { get; set; }
          DateTime? DNAPCR2Date { get; set; }
          DateTime? DNAPCR3Date { get; set; }
          DateTime? ConfirmatoryPCRDate { get; set; }
          DateTime? BasellineVLDate { get; set; }
          DateTime? FinalyAntibodyDate { get; set; }
          string DNAPCR1 { get; set; }
          string DNAPCR2 { get; set; }
          string DNAPCR3 { get; set; }
          string ConfirmatoryPCR { get; set; }
          string BasellineVL { get; set; }
          string FinalyAntibody { get; set; }
          DateTime? HEIExitDate { get; set; }
          string HEIHIVStatus { get; set; }
          string HEIExitCritearia { get; set; }
        

    }
}
