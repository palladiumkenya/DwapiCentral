using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class AllergiesChronicIllnessExtract : IAllergiesChronicIllness
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? ChronicIllness { get; set; }
        public DateTime? ChronicOnsetDate { get; set; }
        public string? knownAllergies { get; set; }
        public string? AllergyCausativeAgent { get; set; }
        public string? AllergicReaction { get; set; }
        public string? AllergySeverity { get; set; }
        public DateTime? AllergyOnsetDate { get; set; }
        public string? Skin { get; set; }
        public string? Eyes { get; set; }
        public string? ENT { get; set; }
        public string? Chest { get; set; }
        public string? CVS { get; set; }
        public string? Abdomen { get; set; }
        public string? CNS { get; set; }
        public string? Genitourinary { get; set; }             
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
