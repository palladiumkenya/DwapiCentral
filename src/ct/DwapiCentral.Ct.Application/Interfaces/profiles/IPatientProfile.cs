using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IPatientProfile : IProfile
    {
        List<PatientArtSourceDto> ArtExtracts { get; set; }
        List<PatientBaselineSourceDto> BaselinesExtracts { get; set; }
        List<LaboratorySourceDto> LaboratoryExtracts { get; set; }
        List<PharmacySourceDto> PharmacyExtracts { get; set; }
        List<StatusSourceDto> StatusExtracts { get; set; }
        List<PatientVisitSourceDto> VisitExtracts { get; set; }
    }
}