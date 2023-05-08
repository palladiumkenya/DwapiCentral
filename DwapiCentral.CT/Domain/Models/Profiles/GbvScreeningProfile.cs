using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Shared.Application.Interfaces.Ct.Profiles;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;


namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class GbvScreeningProfile : ExtractProfile<GbvScreeningExtract>, IGbvScreeningProfile
    {
        public List<GbvScreeningExtractDTO> GbvScreeningExtracts { get; set; } = new List<GbvScreeningExtractDTO>();

        public static GbvScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new GbvScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                GbvScreeningExtracts =
                    new GbvScreeningExtractDTO().GenerateGbvScreeningExtractDtOs(patient.GbvScreeningExtracts)
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

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in GbvScreeningExtracts)
                Extracts.Add(e.GenerateGbvScreeningExtract(PatientInfo.Id));
        }
    }
}
