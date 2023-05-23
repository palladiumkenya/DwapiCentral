using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{

    public class PatientExtract : IPatient
    {
        
        public int PatientPk { get; set; }
        
        public int SiteCode { get; set; }
        
        public string CccNumber { get; set; }
        public string? Nupi { get; set; }
        public string? MpiId { get; set; }
        public string? Pkv { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? RegistrationATPMTCT { get; set; }
        public DateTime? RegistrationAtTBClinic { get; set; }
        public string? PatientSource { get; set; }
        public string? Region { get; set; }
        public string? District { get; set; }
        public string? Village { get; set; }
        public string? ContactRelation { get; set; }
        public DateTime? LastVisit { get; set; }
        public string? MaritalStatus { get; set; }
        public string? EducationLevel { get; set; }
        public DateTime? DateConfirmedHIVPositive { get; set; }
        public string? PreviousARTExposure { get; set; }
        public DateTime? PreviousARTStartDate { get; set; }
        public string? StatusAtCCC { get; set; }
        public string? StatusAtPMTCT { get; set; }
        public string? StatusAtTBClinic { get; set; }
        public string? Orphan { get; set; }
        public string? Inschool { get; set; }
        public string? PatientType { get; set; }
        public string? PopulationType { get; set; }
        public string? KeyPopulationType { get; set; }
        public string? PatientResidentCounty { get; set; }
        public string? PatientResidentSubCounty { get; set; }
        public string? PatientResidentLocation { get; set; }
        public string? PatientResidentSubLocation { get; set; }
        public string? PatientResidentWard { get; set; }
        public string? PatientResidentVillage { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string? Occupation { get; set; }

        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public virtual ICollection<PatientVisitExtract> PatientVisitExtracts { get; set; } = new List<PatientVisitExtract>();
        public virtual ICollection<PatientPharmacyExtract> PatientPharmacyExtracts { get; set; } = new List<PatientPharmacyExtract>();
        public virtual ICollection<PatientLaboratoryExtract> PatientLaboratoryExtracts { get; set; } = new List<PatientLaboratoryExtract>();
        public virtual ICollection<PatientArtExtract> PatientArtExtracts { get;set; }= new List<PatientArtExtract>();

    }
}
