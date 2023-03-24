using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public  interface IHtsClientTests : IEntity
    {
          string FacilityName { get; set; }
          int SiteCode { get; set; }
          int PatientPk { get; set; }
          string HtsNumber { get; set; }
       
       
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          int? EncounterId { get; set; }
          DateTime? TestDate { get; set; }
          string EverTestedForHiv { get; set; }
          int? MonthsSinceLastTest { get; set; }
          string ClientTestedAs { get; set; }
          string EntryPoint { get; set; }
          string TestStrategy { get; set; }
          string TestResult1 { get; set; }
          string TestResult2 { get; set; }
          string FinalTestResult { get; set; }
          string PatientGivenResult { get; set; }
          string TbScreening { get; set; }
          string ClientSelfTested { get; set; }
          string CoupleDiscordant { get; set; }
          string TestType { get; set; }
          string Consent { get; set; }
          Guid FacilityId { get; set; }
    }
}
