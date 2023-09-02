using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientARTProfile : ExtractProfile<PatientArtExtract>, IPatientARTProfile
    {
        public List<PatientArtSourceDto> ArtExtracts { get; set; } = new List<PatientArtSourceDto>();

        public static PatientARTProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientARTProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                ArtExtracts =
                    new PatientArtSourceDto().GeneratePatientArtExtractDtOs(patient.PatientArtExtracts).ToList()
            };
            return patientProfile;
        }

        public static List<PatientARTProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientARTProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }

        public override bool IsValid()
        {
            return base.IsValid() && ArtExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != ArtExtracts;
        }

       
    }
}
