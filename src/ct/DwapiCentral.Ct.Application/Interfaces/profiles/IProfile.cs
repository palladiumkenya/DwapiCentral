using DwapiCentral.Ct.Application.DTOs;

namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IProfile
    {
        FacilityDTO Facility { get; set; }
        PatientExtractDTO Demographic { get; set; }
    
    }
}