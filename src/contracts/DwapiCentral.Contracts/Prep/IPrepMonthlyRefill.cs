using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Prep
{
    public interface IPrepMonthlyRefill : IExtract
    {
    
        string? FacilityName { get; set; }
        string? PrepNumber { get; set; }

        string? VisitDate { get; set; }
        string? BehaviorRiskAssessment { get; set; }
        string? SexPartnerHIVStatus { get; set; }
        string? SymptomsAcuteHIV { get; set; }
        string? AdherenceCounsellingDone { get; set; }
        string? ContraIndicationForPrEP { get; set; }
        string? PrescribedPrepToday { get; set; }
        string? RegimenPrescribed { get; set; }
        string? NumberOfMonths { get; set; }
        string? CondomsIssued { get; set; }
        int? NumberOfCondomsIssued { get; set; }
        string? ClientGivenNextAppointment { get; set; }
        DateTime? AppointmentDate { get; set; }
        string? ReasonForFailureToGiveAppointment { get; set; }
        DateTime? DateOfLastPrepDose { get; set; }

    }
}
