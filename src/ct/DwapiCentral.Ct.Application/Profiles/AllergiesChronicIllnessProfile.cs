using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class AllergiesChronicIllnessProfile : ExtractProfile<AllergiesChronicIllnessExtract>, IAllergiesChronicIllnessProfile
    {
        public List<AllergiesChronicIllnessSourceDto> AllergiesChronicIllnessExtracts { get; set; } = new List<AllergiesChronicIllnessSourceDto>();

        public static AllergiesChronicIllnessProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new AllergiesChronicIllnessProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                AllergiesChronicIllnessExtracts =
                    new AllergiesChronicIllnessSourceDto().GenerateAllergiesChronicIllnessExtractDtOs(patient.AllergiesChronicIllnessExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<AllergiesChronicIllnessProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<AllergiesChronicIllnessProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && AllergiesChronicIllnessExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != AllergiesChronicIllnessExtracts;
        }

        
    }
}
