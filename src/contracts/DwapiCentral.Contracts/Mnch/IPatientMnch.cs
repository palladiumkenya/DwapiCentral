using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
     public interface IPatientMnch : IEntity
    {
       
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          string FacilityName { get; set; }
          string Pkv { get; set; }
          string PatientMnchID { get; set; }
          string PatientHeiID { get; set; }
          string Gender { get; set; }
          DateTime? DOB { get; set; }
          DateTime? FirstEnrollmentAtMnch { get; set; }
          string Occupation { get; set; }
          string MaritalStatus { get; set; }
          string EducationLevel { get; set; }
          string PatientResidentCounty { get; set; }
          string PatientResidentSubCounty { get; set; }
          string PatientResidentWard { get; set; }
          string InSchool { get; set; }

          string NUPI { get; set; }
    }
}
