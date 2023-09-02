using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class EnhancedAdherenceCounsellingProfile : ExtractProfile<EnhancedAdherenceCounsellingExtract>, IEnhancedAdherenceCounsellingProfile
    {
        public List<EnhancedAdherenceCounselingSourceDto> EnhancedAdherenceCounsellingExtracts { get; set; } = new List<EnhancedAdherenceCounselingSourceDto>();

        public static EnhancedAdherenceCounsellingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new EnhancedAdherenceCounsellingProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                EnhancedAdherenceCounsellingExtracts =
                    new EnhancedAdherenceCounselingSourceDto().GenerateEnhancedAdherenceCounsellingExtractDtOs(patient.EnhancedAdherenceCounsellingExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<EnhancedAdherenceCounsellingProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<EnhancedAdherenceCounsellingProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && EnhancedAdherenceCounsellingExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != EnhancedAdherenceCounsellingExtracts;
        }

       
    }
}
