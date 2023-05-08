using System;


namespace DwapiCentral.Shared.Domain.Model.Ct.DTOs
{
   public class PatientExtractId
   {
      public Guid Id { get; set; }
      public int PatientPID { get; set; }
      public Guid FacilityId { get; set; }
   }
}
