using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface ICovid : IExtract
    {
          Guid Id { get; set; }
          string? FacilityName { get; set; }
          int? VisitID { get; set; }
          DateTime? Covid19AssessmentDate { get; set; }
          string? ReceivedCOVID19Vaccine { get; set; }
          DateTime? DateGivenFirstDose { get; set; }
          string? FirstDoseVaccineAdministered { get; set; }
          DateTime? DateGivenSecondDose { get; set; }
          string? SecondDoseVaccineAdministered { get; set; }
          string? VaccinationStatus { get; set; }
          string? VaccineVerification { get; set; }
          string? BoosterGiven { get; set; }
          string? BoosterDose { get; set; }
          DateTime? BoosterDoseDate { get; set; }
          string? EverCOVID19Positive { get; set; }
          DateTime? COVID19TestDate { get; set; }
          string? PatientStatus { get; set; }
          string? AdmissionStatus { get; set; }
          string? AdmissionUnit { get; set; }
          string? MissedAppointmentDueToCOVID19 { get; set; }
          string? COVID19PositiveSinceLasVisit { get; set; }
          DateTime? COVID19TestDateSinceLastVisit { get; set; }
          string? PatientStatusSinceLastVisit { get; set; }
          string? AdmissionStatusSinceLastVisit { get; set; }
          DateTime? AdmissionStartDate { get; set; }
          DateTime? AdmissionEndDate { get; set; }
          string? AdmissionUnitSinceLastVisit { get; set; }
          string? SupplementalOxygenReceived { get; set; }
          string? PatientVentilated { get; set; }
          string? TracingFinalOutcome { get; set; }
          string? CauseOfDeath { get; set; }
          string? COVID19TestResult { get; set; }
          string? Sequence { get; set; }
          string? BoosterDoseVerified { get; set; }
         

    }
}
