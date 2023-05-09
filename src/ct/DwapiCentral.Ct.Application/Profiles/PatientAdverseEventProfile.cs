using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Application.Interfaces.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientAdverseEventProfile : ExtractProfile<PatientAdverseEventExtract>, IPatientAdverseEventProfile
    {
        public List<PatientAdverseEventExtractDTO> AdverseEventExtracts { get; set; } = new List<PatientAdverseEventExtractDTO>();

        public static PatientAdverseEventProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientAdverseEventProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                AdverseEventExtracts =
                    new PatientAdverseEventExtractDTO().GeneratePatientAdverseEventExtractDtOs(patient.PatientAdverseEventExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<PatientAdverseEventProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientAdverseEventProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }

        public override bool IsValid()
        {
            return base.IsValid() && AdverseEventExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != AdverseEventExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in AdverseEventExtracts)
                Extracts.Add(e.GeneratePatientAdverseEventExtract(PatientInfo.Id));
        }
    }
}
