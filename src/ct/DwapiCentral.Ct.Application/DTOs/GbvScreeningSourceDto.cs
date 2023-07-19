using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class GbvScreeningSourceDto : IGbvScreening
    {
        public Guid Id { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? IPV { get; set; }
        public string? PhysicalIPV { get; set; }
        public string? EmotionalIPV { get; set; }
        public string? SexualIPV { get; set; }
        public string? IPVRelationship { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
