using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class PatientArtSourceDto : IArt
    {
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }       
        public DateTime LastARTDate { get; set; }        
        public DateTime? DOB { get ; set ; }
        public decimal? AgeEnrollment { get ; set ; }
        public decimal? AgeARTStart { get ; set ; }
        public decimal? AgeLastVisit { get ; set ; }
        public DateTime? RegistrationDate { get ; set ; }
        public string? Gender { get ; set ; }
        public string? PatientSource { get ; set ; }
        public DateTime? StartARTDate { get ; set ; }
        public DateTime? PreviousARTStartDate { get ; set ; }
        public string? PreviousARTRegimen { get ; set ; }
        public DateTime? StartARTAtThisFacility { get ; set ; }
        public string? StartRegimen { get ; set ; }
        public string? StartRegimenLine { get ; set ; }
        public DateTime? LastVisit { get; set; }
        public string? LastRegimen { get ; set ; }
        public string? LastRegimenLine { get ; set ; }
        public decimal? Duration { get ; set ; }
        public DateTime? ExpectedReturn { get ; set ; }
        public string? Provider { get ; set ; }
        public string? ExitReason { get ; set ; }
        public DateTime? ExitDate { get ; set ; }
        public string? PreviousARTUse { get ; set ; }
        public string? PreviousARTPurpose { get ; set ; }
        public DateTime? DateLastUsed { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public PatientArtSourceDto()
        {

        }

        public PatientArtSourceDto(PatientArtExtract patientArtExtract)
        {
            DOB = patientArtExtract.DOB;
            AgeEnrollment = patientArtExtract.AgeEnrollment;
            AgeARTStart = patientArtExtract.AgeARTStart;
            AgeLastVisit = patientArtExtract.AgeLastVisit;
            RegistrationDate = patientArtExtract.RegistrationDate;
            Gender = patientArtExtract.Gender;
            PatientSource = patientArtExtract.PatientSource;
            StartARTDate = patientArtExtract.StartARTDate;
            PreviousARTStartDate = patientArtExtract.PreviousARTStartDate;
            PreviousARTRegimen = patientArtExtract.PreviousARTRegimen;
            StartARTAtThisFacility = patientArtExtract.StartARTAtThisFacility;
            StartRegimen = patientArtExtract.StartRegimen;
            StartRegimenLine = patientArtExtract.StartRegimenLine;
            LastARTDate = patientArtExtract.LastARTDate;
            LastRegimen = patientArtExtract.LastRegimen;
            LastRegimenLine = patientArtExtract.LastRegimenLine;
            Duration = patientArtExtract.Duration;
            ExpectedReturn = patientArtExtract.ExpectedReturn;
            LastVisit = patientArtExtract.LastVisit;
            ExitReason = patientArtExtract.ExitReason;
            ExitDate = patientArtExtract.ExitDate;
            SiteCode = patientArtExtract.SiteCode;
            PatientPk = patientArtExtract.PatientPk;
           
            Date_Created = patientArtExtract.Date_Created;
            Date_Last_Modified = patientArtExtract.Date_Last_Modified;
            RecordUUID = patientArtExtract.RecordUUID;

        }

        public IEnumerable<PatientArtSourceDto> GeneratePatientArtExtractDtOs(IEnumerable<PatientArtExtract> extracts)
        {
            var artExtracts = new List<PatientArtSourceDto>();
            foreach (var e in extracts.ToList())
            {
                artExtracts.Add(new PatientArtSourceDto(e));
            }
            return artExtracts;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
