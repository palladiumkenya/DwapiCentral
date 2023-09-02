using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class PatientVisitProfile : ExtractProfile<PatientVisitExtract>, IPatientVisitProfile
    {
        public List<PatientVisitSourceDto> VisitExtracts { get; set; } = new List<PatientVisitSourceDto>();

        public static PatientVisitProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new PatientVisitProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                VisitExtracts =
                    new PatientVisitSourceDto().GeneratePatientVisitExtractDtOs(patient.PatientVisitExtracts).ToList()
            };
            return patientProfile;
        }

        public static List<PatientVisitProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<PatientVisitProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && VisitExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != VisitExtracts;
        }

 
    }
}
