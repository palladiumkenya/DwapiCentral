using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class GbvScreeningProfile : ExtractProfile<GbvScreeningExtract>, IGbvScreeningProfile
    {
        public List<GbvScreeningSourceDto> GbvScreeningExtracts { get; set; } = new List<GbvScreeningSourceDto>();

        public static GbvScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new GbvScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                GbvScreeningExtracts =
                    new GbvScreeningSourceDto().GenerateGbvScreeningExtractDtOs(patient.GbvScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<GbvScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<GbvScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && GbvScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != GbvScreeningExtracts;
        }

       
    }
}
