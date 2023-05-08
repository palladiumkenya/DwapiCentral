using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Shared.Application.Interfaces.Ct.Profiles;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;

namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class DepressionScreeningProfile : ExtractProfile<DepressionScreeningExtract>, IDepressionScreeningProfile
    {
        public List<DepressionScreeningExtractDTO> DepressionScreeningExtracts { get; set; } = new List<DepressionScreeningExtractDTO>();

        public static DepressionScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DepressionScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                DepressionScreeningExtracts =
                    new DepressionScreeningExtractDTO().GenerateDepressionScreeningExtractDtOs(patient.DepressionScreeningExtracts)
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

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in DepressionScreeningExtracts)
                Extracts.Add(e.GenerateDepressionScreeningExtract(PatientInfo.Id));
        }
    }
}
