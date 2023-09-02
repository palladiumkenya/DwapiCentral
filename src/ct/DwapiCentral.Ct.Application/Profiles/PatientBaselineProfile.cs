using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientBaselineProfile : ExtractProfile<PatientBaselinesExtract>, IPatientBaselineProfile
    {
        public List<PatientBaselineSourceDto> BaselinesExtracts { get; set; } = new List<PatientBaselineSourceDto>();



        public static PatientBaselineProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientBaselineProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                BaselinesExtracts =
                    new PatientBaselineSourceDto().GeneratePatientBaselinesExtractDtOs(
                        patient.PatientBaselinesExtracts).ToList()
            };
            return patientProfile;
        }

        public static List<PatientBaselineProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientBaselineProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && BaselinesExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != BaselinesExtracts;
        }

      
    }
}
