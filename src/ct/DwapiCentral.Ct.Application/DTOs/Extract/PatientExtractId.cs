using System;


namespace DwapiCentral.Ct.Application.DTOs.Extract
{
   public class PatientExtractId
   {
      public Guid Id { get; set; }
      public int PatientPID { get; set; }
      public Guid FacilityId { get; set; }
   }
}
