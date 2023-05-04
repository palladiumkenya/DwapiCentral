using System;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using PalladiumDwh.Shared.Interfaces;

namespace DwapiCentral.Shared.Application.DTOs
{
    public class LaboratorySourceDto : SourceDto, ILaboratory
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
    }
}