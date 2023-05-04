using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
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