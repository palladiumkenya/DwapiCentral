using DwapiCentral.Contracts.Mnch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model
{
    public class PatientMnchExtract : IPatientMnch
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string? FacilityName { get; set; }
        public string? Pkv { get; set; }
        public string PatientMnchID { get; set; }
        public string? PatientHeiID { get; set; }
        public string? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public DateTime? FirstEnrollmentAtMnch { get; set; }
        public string? Occupation { get; set; }
        public string? MaritalStatus { get; set; }
        public string? EducationLevel { get; set; }
        public string? PatientResidentCounty { get; set; }
        public string? PatientResidentSubCounty { get; set; }
        public string? PatientResidentWard { get; set; }
        public string? InSchool { get; set; }
        public string? NUPI { get; set; }        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }


        public virtual ICollection<MnchEnrolment> MnchEnrolmentExtracts { get; set; } = new List<MnchEnrolment>();
        public virtual ICollection<MnchArt> MnchArtExtracts { get; set; } = new List<MnchArt>();
        public virtual ICollection<AncVisit> AncVisitExtracts { get; set; } = new List<AncVisit>();
        public virtual ICollection<MatVisit> MatVisitExtracts { get; set; } = new List<MatVisit>();
        public virtual ICollection<PncVisit> PncVisitExtracts { get; set; } = new List<PncVisit>();
        public virtual ICollection<MotherBabyPair> MotherBabyPairExtracts { get; set; } = new List<MotherBabyPair>();
        public virtual ICollection<CwcEnrolment> CwcEnrolmentExtracts { get; set; } = new List<CwcEnrolment>();
        public virtual ICollection<CwcVisit> CwcVisitExtracts { get; set; } = new List<CwcVisit>();
        public virtual ICollection<HeiExtract> HeiExtracts { get; set; } = new List<HeiExtract>();
        public virtual ICollection<MnchLab> MnchLabExtracts { get; set; } = new List<MnchLab>();
        public virtual ICollection<MnchImmunization> MnchImmunizationExtracts { get; set; } = new List<MnchImmunization>();
    }
}
