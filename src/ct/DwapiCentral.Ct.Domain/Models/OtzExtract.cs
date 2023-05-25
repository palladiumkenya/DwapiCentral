using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class OtzExtract : IOtz
    {
        [Key]
        public Guid Id { get ; set ; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get ; set ; }        
        public DateTime? OTZEnrollmentDate { get ; set ; }
        public string? TransferInStatus { get ; set ; }
        public string? ModulesPreviouslyCovered { get ; set ; }
        public string? ModulesCompletedToday { get ; set ; }
        public string? SupportGroupInvolvement { get ; set ; }
        public string? Remarks { get ; set ; }
        public string? TransitionAttritionReason { get ; set ; }
        public DateTime? OutcomeDate { get ; set ; }
        public Guid? PatientId { get ; set ; }
    
        public DateTime? DateCreated { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
