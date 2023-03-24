using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
    public interface IPrepLab : IEntity
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        int? VisitID { get; set; }
        string TestName { get; set; }
        string TestResult { get; set; }
        DateTime? SampleDate { get; set; }
        DateTime? TestResultDate { get; set; }
        string Reason { get; set; }
       
    }
}
