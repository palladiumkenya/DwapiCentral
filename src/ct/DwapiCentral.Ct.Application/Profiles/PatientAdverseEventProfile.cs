using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientAdverseEventProfile : ExtractProfile<PatientAdverseEventExtract>, IPatientAdverseEventProfile
    {
        public List<AdverseEventSourceDto> AdverseEventExtracts { get; set; } = new List<AdverseEventSourceDto>();

        public static PatientAdverseEventProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientAdverseEventProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                AdverseEventExtracts =
                    new AdverseEventSourceDto().GeneratePatientAdverseEventExtractDtOs(patient.PatientAdverseEventExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<PatientAdverseEventProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientAdverseEventProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }

        public override bool IsValid()
        {
            return base.IsValid() && AdverseEventExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != AdverseEventExtracts;
        }

    }
}
