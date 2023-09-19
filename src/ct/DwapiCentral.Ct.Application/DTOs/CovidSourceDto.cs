using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class CovidSourceDto : ICovid
    {
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }
        public string? FacilityName { get ; set ; }
        public int VisitID { get ; set ; }
        public DateTime Covid19AssessmentDate { get ; set ; }
        public string? ReceivedCOVID19Vaccine { get ; set ; }
        public DateTime? DateGivenFirstDose { get ; set ; }
        public string? FirstDoseVaccineAdministered { get ; set ; }
        public DateTime? DateGivenSecondDose { get ; set ; }
        public string? SecondDoseVaccineAdministered { get ; set ; }
        public string? VaccinationStatus { get ; set ; }
        public string? VaccineVerification { get ; set ; }
        public string? BoosterGiven { get ; set ; }
        public string? BoosterDose { get ; set ; }
        public DateTime? BoosterDoseDate { get ; set ; }
        public string? EverCOVID19Positive { get ; set ; }
        public DateTime? COVID19TestDate { get ; set ; }
        public string? PatientStatus { get ; set ; }
        public string? AdmissionStatus { get ; set ; }
        public string? AdmissionUnit { get ; set ; }
        public string? MissedAppointmentDueToCOVID19 { get ; set ; }
        public string? COVID19PositiveSinceLasVisit { get ; set ; }
        public DateTime? COVID19TestDateSinceLastVisit { get ; set ; }
        public string? PatientStatusSinceLastVisit { get ; set ; }
        public string? AdmissionStatusSinceLastVisit { get ; set ; }
        public DateTime? AdmissionStartDate { get ; set ; }
        public DateTime? AdmissionEndDate { get ; set ; }
        public string? AdmissionUnitSinceLastVisit { get ; set ; }
        public string? SupplementalOxygenReceived { get ; set ; }
        public string? PatientVentilated { get ; set ; }
        public string? TracingFinalOutcome { get ; set ; }
        public string? CauseOfDeath { get ; set ; }
        public string? COVID19TestResult { get ; set ; }
        public string? Sequence { get ; set ; }
        public string? BoosterDoseVerified { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public CovidSourceDto()
        {
        }

        public CovidSourceDto(CovidExtract CovidExtract)
        {
            FacilityName = CovidExtract.FacilityName;
            VisitID = CovidExtract.VisitID;
            Covid19AssessmentDate = CovidExtract.Covid19AssessmentDate;
            ReceivedCOVID19Vaccine = CovidExtract.ReceivedCOVID19Vaccine;
            DateGivenFirstDose = CovidExtract.DateGivenFirstDose;
            FirstDoseVaccineAdministered = CovidExtract.FirstDoseVaccineAdministered;
            DateGivenSecondDose = CovidExtract.DateGivenSecondDose;
            SecondDoseVaccineAdministered = CovidExtract.SecondDoseVaccineAdministered;
            VaccinationStatus = CovidExtract.VaccinationStatus;
            VaccineVerification = CovidExtract.VaccineVerification;
            BoosterGiven = CovidExtract.BoosterGiven;
            BoosterDose = CovidExtract.BoosterDose;
            BoosterDoseDate = CovidExtract.BoosterDoseDate;
            EverCOVID19Positive = CovidExtract.EverCOVID19Positive;
            COVID19TestDate = CovidExtract.COVID19TestDate;
            PatientStatus = CovidExtract.PatientStatus;
            AdmissionStatus = CovidExtract.AdmissionStatus;
            AdmissionUnit = CovidExtract.AdmissionUnit;
            MissedAppointmentDueToCOVID19 = CovidExtract.MissedAppointmentDueToCOVID19;
            COVID19PositiveSinceLasVisit = CovidExtract.COVID19PositiveSinceLasVisit;
            COVID19TestDateSinceLastVisit = CovidExtract.COVID19TestDateSinceLastVisit;
            PatientStatusSinceLastVisit = CovidExtract.PatientStatusSinceLastVisit;
            AdmissionStatusSinceLastVisit = CovidExtract.AdmissionStatusSinceLastVisit;
            AdmissionStartDate = CovidExtract.AdmissionStartDate;
            AdmissionEndDate = CovidExtract.AdmissionEndDate;
            AdmissionUnitSinceLastVisit = CovidExtract.AdmissionUnitSinceLastVisit;
            SupplementalOxygenReceived = CovidExtract.SupplementalOxygenReceived;
            PatientVentilated = CovidExtract.PatientVentilated;
            TracingFinalOutcome = CovidExtract.TracingFinalOutcome;
            CauseOfDeath = CovidExtract.CauseOfDeath;
            COVID19TestResult = CovidExtract.COVID19TestResult;
            Sequence = CovidExtract.Sequence;
            BoosterDoseVerified = CovidExtract.BoosterDoseVerified;
            Date_Created = CovidExtract.Date_Created;
            Date_Last_Modified = CovidExtract.Date_Last_Modified;
            RecordUUID = CovidExtract.RecordUUID;

            SiteCode = CovidExtract.SiteCode;
            PatientPk= CovidExtract.PatientPk;
        }

        public IEnumerable<CovidSourceDto> GenerateCovidExtractDtOs(IEnumerable<CovidExtract> extracts)
        {
            var statusExtractDtos = new List<CovidSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new CovidSourceDto(e));
            }
            return statusExtractDtos;
        }


        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}