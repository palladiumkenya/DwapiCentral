using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientPharmacyProfile : ExtractProfile<PatientPharmacyExtract>, IPatientPharmacyProfile
    {
        public List<PharmacySourceDto> PharmacyExtracts { get; set; } = new List<PharmacySourceDto>();

        public static PatientPharmacyProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientPharmacyProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                PharmacyExtracts =
                    new PharmacySourceDto().GeneratePatientPharmacyExtractDtOs(patient.PatientPharmacyExtracts)
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

        
    }
}
