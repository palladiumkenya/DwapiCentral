using System.Collections.Generic;
using DwapiCentral.Shared.Domain.Model.Common;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;


namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class SitePatientProfile
    {
        public Manifest Manifest { get; set; }
        public FacilityDTO Facility { get; set; }
        public PatientExtractDTO Demographic { get; set; }
        public Facility FacilityInfo { get; set; }
        public PatientExtract PatientInfo { get; set; }

        public List<PatientArtExtractDTO> ArtExtracts { get; set; } = new List<PatientArtExtractDTO>();
        public List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; } = new List<PatientBaselinesExtractDTO>();
        public List<PatientLaboratoryExtractDTO> LaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtractDTO>();
        public List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; } = new List<PatientPharmacyExtractDTO>();
        public List<PatientStatusExtractDTO> StatusExtracts { get; set; } = new List<PatientStatusExtractDTO>();
        public List<PatientVisitExtractDTO> VisitExtracts { get; set; } = new List<PatientVisitExtractDTO>();

        public bool IsValid()
        {
            return true;
        }
    }
}