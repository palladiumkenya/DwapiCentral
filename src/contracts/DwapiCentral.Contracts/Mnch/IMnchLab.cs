using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
     public interface IMnchLab : IEntity
    {
       
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          Guid FacilityId { get; set; }
          string PatientMNCH_ID { get; set; }
          string FacilityName { get; set; }
          string SatelliteName { get; set; }
          int? VisitID { get; set; }
          DateTime? OrderedbyDate { get; set; }
          DateTime? ReportedbyDate { get; set; }
          string TestName { get; set; }
          string TestResult { get; set; }
          string LabReason { get; set; }
       

    }
}
