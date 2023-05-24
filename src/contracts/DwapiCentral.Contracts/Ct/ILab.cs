using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface ILab : IExtract
    {
        public Guid Id { get; set; }
        int VisitId { get; set; }
        DateTime OrderedByDate { get; set; }
        DateTime? ReportedByDate { get; set; }
        string? TestName { get; set; }
        int? EnrollmentTest { get; set; }
        string? TestResult { get; set; }
        Guid? PatientId { get; set; }
        DateTime? DateSampleTaken { get; set; }
        string? SampleType { get; set; }   
        

      
    }
}