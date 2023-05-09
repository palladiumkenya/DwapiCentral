using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Application.Interfaces.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class OvcProfile : ExtractProfile<OvcExtract>, IOvcProfile
    {
        public List<OvcExtractDTO> OvcExtracts { get; set; } = new List<OvcExtractDTO>();

        public static OvcProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new OvcProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                OvcExtracts =
                    new OvcExtractDTO().GenerateOvcExtractDtOs(patient.OvcExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<OvcProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<OvcProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && OvcExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != OvcExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in OvcExtracts)
                Extracts.Add(e.GenerateOvcExtract(PatientInfo.Id));
        }
    }
}
