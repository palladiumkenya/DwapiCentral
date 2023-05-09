using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Application.Interfaces.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class CovidProfile : ExtractProfile<CovidExtract>, ICovidProfile
    {
        public List<CovidExtractDTO> CovidExtracts { get; set; } = new List<CovidExtractDTO>();

        public static CovidProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new CovidProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                CovidExtracts =
                    new CovidExtractDTO().GenerateCovidExtractDtOs(patient.CovidExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<CovidProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<CovidProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && CovidExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != CovidExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in CovidExtracts)
                Extracts.Add(e.GenerateCovidExtract(PatientInfo.Id));
        }
    }
}
