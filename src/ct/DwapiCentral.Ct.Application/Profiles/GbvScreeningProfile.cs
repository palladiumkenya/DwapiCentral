using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class GbvScreeningProfile 
    {
        public List<GbvScreeningExtract> GbvScreeningExtracts { get; set; }

        public PatientExtractDTO Demographic { get; set; }

        public FacilityDTO Facility { get; set; }

    }
}
