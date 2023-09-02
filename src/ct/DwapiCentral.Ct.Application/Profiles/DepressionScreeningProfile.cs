using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class DepressionScreeningProfile : ExtractProfile<DepressionScreeningExtract>, IDepressionScreeningProfile
    {
        public List<DepressionScreeningSourceDto> DepressionScreeningExtracts { get; set; } = new List<DepressionScreeningSourceDto>();

        public static DepressionScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DepressionScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                DepressionScreeningExtracts =
                    new DepressionScreeningSourceDto().GenerateDepressionScreeningExtractDtOs(patient.DepressionScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<DepressionScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<DepressionScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && DepressionScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != DepressionScreeningExtracts;
        }

        
    }
}
