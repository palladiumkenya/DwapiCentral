using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models
{
    public class PatientLaboratoryExtract : ILab
    {
        [Key]
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }       
        public int VisitId { get; set; }
        public DateTime OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string? TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string? TestResult { get; set; }       
        public DateTime? DateSampleTaken { get; set; }
        public string? SampleType { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public string? Reason { get; set; }
       

        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

   
    }
}
