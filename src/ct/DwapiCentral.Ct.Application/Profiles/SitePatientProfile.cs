using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Domain.Models;

using Newtonsoft.Json;


namespace PalladiumDwh.Shared.Model.Profiles
{
    public class SitePatientProfile 
    {        
        public Manifest Manifest { get; set; }
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public Facility FacilityInfo { get; set; }
        public PatientExtract PatientInfo { get; set; }

        public List<PatientArtSourceDto> ArtExtracts { get; set; } = new List<PatientArtSourceDto>();
        public List<PatientBaselineSourceDto> BaselinesExtracts { get; set; } = new List<PatientBaselineSourceDto>();
        public List<LaboratorySourceDto> LaboratoryExtracts { get; set; } =new List<LaboratorySourceDto>();
        public List<PharmacySourceDto> PharmacyExtracts { get; set; } = new List<PharmacySourceDto>();
        public List<StatusSourceDto> StatusExtracts { get; set; } = new List<StatusSourceDto>();
        public List<PatientVisitSourceDto> VisitExtracts { get; set; } = new List<PatientVisitSourceDto>();

        public bool IsValid()
        {
            return true;
        }
    }
}