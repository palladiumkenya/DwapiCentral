using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class IptProfile : ExtractProfile<IptExtract>, IIptProfile
    {
        public List<PatientIptSourceDto> IptExtracts { get; set; } = new List<PatientIptSourceDto>();

        public static IptProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new IptProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                IptExtracts =
                    new PatientIptSourceDto().GenerateIptExtractDtOs(patient.IptExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<IptProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<IptProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && IptExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != IptExtracts;
        }

       
    }
}
