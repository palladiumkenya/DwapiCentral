using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
     public interface IMnchLab : IExtract
    {
        string PatientMNCH_ID { get; set; }
        string? FacilityName { get; set; }
        string? SatelliteName { get; set; }
        int? VisitID { get; set; }
        DateTime? OrderedbyDate { get; set; }
        DateTime? ReportedbyDate { get; set; }
        string? TestName { get; set; }
        string? TestResult { get; set; }
        string? LabReason { get; set; }

       
    }
}
