using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Application.Interfaces.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientStatusProfile : ExtractProfile<PatientStatusExtract>, IPatientStatusProfile
    {
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();

        public static PatientStatusProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientStatusProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                StatusExtracts =
                    new PatientStatusExtractDTO().GeneratePatientStatusExtractDtOs(patient.PatientStatusExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<PatientStatusProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientStatusProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && StatusExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != StatusExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in StatusExtracts)
                Extracts.Add(e.GeneratePatientStatusExtract(PatientInfo.Id));
        }
    }
}
