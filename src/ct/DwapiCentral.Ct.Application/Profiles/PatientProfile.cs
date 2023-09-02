using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Domain.Models;

using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientProfile : IPatientProfile
    {
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }

        public Facility FacilityInfo { get; set; }
        List<PatientArtSourceDto> IPatientProfile.ArtExtracts { get ; set ; }
        List<PatientBaselineSourceDto> IPatientProfile.BaselinesExtracts { get ; set ; }
        List<LaboratorySourceDto> IPatientProfile.LaboratoryExtracts { get ; set ; }
        List<PharmacySourceDto> IPatientProfile.PharmacyExtracts { get ; set ; }
        List<StatusSourceDto> IPatientProfile.StatusExtracts { get ; set ; }
        List<PatientVisitSourceDto> IPatientProfile.VisitExtracts { get ; set ; }

        public void GeneratePatientRecord()
        {
            FacilityInfo = Facility.GenerateFacility();
           
        }

        public static PatientProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientProfile
            {
                Facility = new FacilityDTO(facility)
               
            };
            return patientProfile;
        }
      
    }
}