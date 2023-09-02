using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class DefaulterTracingProfile : ExtractProfile<DefaulterTracingExtract>, IDefaulterTracingProfile
    {
        public List<DefaulterTracingSourceDto> DefaulterTracingExtracts { get; set; } = new List<DefaulterTracingSourceDto>();

        public static DefaulterTracingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DefaulterTracingProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                DefaulterTracingExtracts =
                    new DefaulterTracingSourceDto().GenerateDefaulterTracingExtractDtOs(patient.DefaulterTracingExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<DefaulterTracingProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<DefaulterTracingProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && DefaulterTracingExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != DefaulterTracingExtracts;
        }

       
    }
}
