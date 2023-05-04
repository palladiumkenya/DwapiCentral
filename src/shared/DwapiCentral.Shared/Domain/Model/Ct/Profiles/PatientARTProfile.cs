using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Shared.Application.Interfaces.Ct.Profiles;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;

namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class PatientARTProfile : ExtractProfile<PatientArtExtract>, IPatientARTProfile
    {
        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();

        public static PatientARTProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientARTProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                ArtExtracts =
                    new PatientArtExtractDTO().GeneratePatientArtExtractDtOs(patient.PatientArtExtracts).ToList()
            };
            return patientProfile;
        }

        public static List<PatientARTProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientARTProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }

        public override bool IsValid()
        {
            return base.IsValid() && ArtExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != ArtExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in ArtExtracts)
                Extracts.Add(e.GeneratePatientArtExtract(PatientInfo.Id));
        }
    }
}
