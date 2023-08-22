using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
     public interface IPatientMnch : IExtract
    {
        string? FacilityName { get; set; }
        string? Pkv { get; set; }
        string PatientMnchID { get; set; }
        string PatientHeiID { get; set; }
        string? Gender { get; set; }
        DateTime? DOB { get; set; }
        DateTime? FirstEnrollmentAtMnch { get; set; }
        string? Occupation { get; set; }
        string? MaritalStatus { get; set; }
        string? EducationLevel { get; set; }
        string? PatientResidentCounty { get; set; }
        string? PatientResidentSubCounty { get; set; }
        string? PatientResidentWard { get; set; }
        string? InSchool { get; set; }        
        string? NUPI { get; set; }
    }
}
