using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class ARTFastTrackProfile
    {
        public List<ArtFastTrackExtract> ArtFastTrackExtracts { get; set; }

        public PatientExtractDTO Demographic { get; set; }

        public FacilityDTO Facility { get; set; }
    }
}
