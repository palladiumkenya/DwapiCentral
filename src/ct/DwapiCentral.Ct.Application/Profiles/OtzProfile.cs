using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class OtzProfile : ExtractProfile<OtzExtract>, IOtzProfile
    {
        public List<OtzSourceDto> OtzExtracts { get; set; } = new List<OtzSourceDto>();

        public static OtzProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new OtzProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                OtzExtracts =
                    new OtzSourceDto().GenerateOtzExtractDtOs(patient.OtzExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<OtzProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<OtzProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && OtzExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != OtzExtracts;
        }


    }
}
