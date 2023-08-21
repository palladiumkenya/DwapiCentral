using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class DrugAlcoholScreeningExtract : IDrugAlcoholScreening
    {
        [Key]
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int VisitID { get ; set ; }
        public DateTime VisitDate { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string? DrinkingAlcohol { get ; set ; }
        public string? Smoking { get ; set ; }
        public string? DrugUse { get ; set ; }             
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
