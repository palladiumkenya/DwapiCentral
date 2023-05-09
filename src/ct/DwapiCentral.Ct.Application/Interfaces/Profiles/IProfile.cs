using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface IProfile
    {
        FacilityDTO Facility { get; set; }
        PatientExtractDTO Demographic { get; set; }
        Facility FacilityInfo { get; set; }
        PatientExtract PatientInfo { get; set; }
        void GeneratePatientRecord();
    }
}