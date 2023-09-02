using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class CovidProfile : ExtractProfile<CovidExtract>, ICovidProfile
    {
        public List<CovidSourceDto> CovidExtracts { get; set; } = new List<CovidSourceDto>();

        public static CovidProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new CovidProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                CovidExtracts =
                    new CovidSourceDto().GenerateCovidExtractDtOs(patient.CovidExtracts)
                        .ToList()
            };
            return patientProfile;
        }



        
    }
}
