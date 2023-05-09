using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs.Extract;


namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface IPatientProfile : IProfile
    {
        List<PatientArtExtractDTO> ArtExtracts { get; set; }
        List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; }
        List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; }
        List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }
        List<PatientStatusExtractDTO> StatusExtracts { get; set; }
        List<PatientVisitExtractDTO> VisitExtracts { get; set; }
    }
}