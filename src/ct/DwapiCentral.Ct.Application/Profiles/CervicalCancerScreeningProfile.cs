using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class CervicalCancerScreeningProfile : ExtractProfile<CervicalCancerScreeningExtract>, ICervicalCancerScreeningProfile
    {
        public List<CervicalCancerScreeningSourceDto> CervicalCancerScreeningExtracts { get; set; } = new List<CervicalCancerScreeningSourceDto>();

        public static CervicalCancerScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new CervicalCancerScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                CervicalCancerScreeningExtracts =
                    new CervicalCancerScreeningSourceDto().GenerateCervicalCancerScreeningExtractDtOs(patient.CervicalCancerScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<CervicalCancerScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<CervicalCancerScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && CervicalCancerScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != CervicalCancerScreeningExtracts;
        }

       
    }
}
