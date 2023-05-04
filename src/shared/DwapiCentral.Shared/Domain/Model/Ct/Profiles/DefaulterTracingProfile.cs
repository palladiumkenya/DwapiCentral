using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Shared.Application.Interfaces.Ct.Profiles;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;

namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class DefaulterTracingProfile : ExtractProfile<DefaulterTracingExtract>, IDefaulterTracingProfile
    {
        public List<DefaulterTracingExtractDTO> DefaulterTracingExtracts { get; set; } = new List<DefaulterTracingExtractDTO>();

        public static DefaulterTracingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DefaulterTracingProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                DefaulterTracingExtracts =
                    new DefaulterTracingExtractDTO().GenerateDefaulterTracingExtractDtOs(patient.DefaulterTracingExtracts)
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

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in DefaulterTracingExtracts)
                Extracts.Add(e.GenerateDefaulterTracingExtract(PatientInfo.Id));
        }
    }
}
