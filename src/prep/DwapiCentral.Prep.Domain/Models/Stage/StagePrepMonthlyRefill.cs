using DwapiCentral.Contracts.Prep;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models.Stage
{
    public class StagePrepMonthlyRefill : IPrepMonthlyRefill
    {
        public Guid Id { get; set; }
        public string? FacilityName { get ; set ; }
        public string? PrepNumber { get ; set ; }
        public string? VisitDate { get ; set ; }
        public string? BehaviorRiskAssessment { get ; set ; }
        public string? SexPartnerHIVStatus { get ; set ; }
        public string? SymptomsAcuteHIV { get ; set ; }
        public string? AdherenceCounsellingDone { get ; set ; }
        public string? ContraIndicationForPrEP { get ; set ; }
        public string? PrescribedPrepToday { get ; set ; }
        public string? RegimenPrescribed { get ; set ; }
        public string? NumberOfMonths { get ; set ; }
        public string? CondomsIssued { get ; set ; }
        public int? NumberOfCondomsIssued { get ; set ; }
        public string? ClientGivenNextAppointment { get ; set ; }
        public DateTime? AppointmentDate { get ; set ; }
        public string? ReasonForFailureToGiveAppointment { get ; set ; }
        public DateTime? DateOfLastPrepDose { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public string RecordUUID { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }


        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }

    }
}
