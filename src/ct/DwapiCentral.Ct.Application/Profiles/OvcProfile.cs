using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class OvcProfile : ExtractProfile<OvcExtract>, IOvcProfile
    {
        public List<OvcSourceDto> OvcExtracts { get; set; } = new List<OvcSourceDto>();

        public static OvcProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new OvcProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                OvcExtracts =
                    new OvcSourceDto().GenerateOvcExtractDtOs(patient.OvcExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<OvcProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<OvcProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && OvcExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != OvcExtracts;
        }

    }
}
