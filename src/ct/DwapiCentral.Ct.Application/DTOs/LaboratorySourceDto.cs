using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class LaboratorySourceDto : SourceDto, ILab
    {
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public int PatientPk { get; set; }
        public bool Voided { get; set; }
        public DateTime? Extracted { get; set; }
        Guid ILab.PatientId { get; set; }
    }
}