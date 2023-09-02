using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class IITRiskScoresProfile : ExtractProfile<IITRiskScore>, IIITRiskScoresProfile
    {
        public List<IITRiskScoreSourceDto> IITRiskScoresExtracts { get; set; } = new List<IITRiskScoreSourceDto>();

        public static IITRiskScoresProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new IITRiskScoresProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                IITRiskScoresExtracts =
                    new IITRiskScoreSourceDto().GenerateIITRiskScoresExtractDtOs(patient.IITRiskScoresExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<IITRiskScoresProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<IITRiskScoresProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && IITRiskScoresExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != IITRiskScoresExtracts;
        }

    }
}
