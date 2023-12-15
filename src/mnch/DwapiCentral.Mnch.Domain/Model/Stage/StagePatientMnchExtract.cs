using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model.Stage
{
    public class StagePatientMnchExtract : IPatientMnch
    {
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public string RecordUUID { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string? Pkv { get ; set ; }
        public string? PatientMnchID { get ; set ; }
        public string? PatientHeiID { get ; set ; }
        public string? Gender { get ; set ; }
        public DateTime? DOB { get ; set ; }
        public DateTime? FirstEnrollmentAtMnch { get ; set ; }
        public string? Occupation { get ; set ; }
        public string? MaritalStatus { get ; set ; }
        public string? EducationLevel { get ; set ; }
        public string? PatientResidentCounty { get ; set ; }
        public string? PatientResidentSubCounty { get ; set ; }
        public string? PatientResidentWard { get ; set ; }
        public string? InSchool { get ; set ; }
        public string? NUPI { get ; set ; }
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
