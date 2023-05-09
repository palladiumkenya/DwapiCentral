using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Application.Interfaces.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientPharmacyProfile : ExtractProfile<PatientPharmacyExtract>, IPatientPharmacyProfile
    {
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();

        public static PatientPharmacyProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientPharmacyProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                PharmacyExtracts =
                    new PatientPharmacyExtractDTO().GeneratePatientPharmacyExtractDtOs(patient.PatientPharmacyExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<PatientPharmacyProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientPharmacyProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && PharmacyExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != PharmacyExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in PharmacyExtracts)
                Extracts.Add(e.GeneratePatientPharmacyExtract(PatientInfo.Id));
        }
    }
}
