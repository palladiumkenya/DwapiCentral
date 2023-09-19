using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public  interface IHtsClientTests : IExtract
    {
                 
          string HtsNumber { get; set; }
          int? EncounterId { get; set; }
          string? FacilityName { get; set; }
          bool? Processed { get; set; }          
          string? Status { get; set; }
          DateTime? StatusDate { get; set; }              
          DateTime? TestDate { get; set; }
          string? EverTestedForHiv { get; set; }
          int? MonthsSinceLastTest { get; set; }
          string? ClientTestedAs { get; set; }
          string? EntryPoint { get; set; }
          string? TestStrategy { get; set; }
          string? TestResult1 { get; set; }
          string? TestResult2 { get; set; }
          string? FinalTestResult { get; set; }
          string? PatientGivenResult { get; set; }
          string? TbScreening { get; set; }
          string? ClientSelfTested { get; set; }
          string? CoupleDiscordant { get; set; }
          string? TestType { get; set; }
          string? Consent { get; set; }
          string? Setting { get; set; }
          string? Approach { get; set; }
          string? HtsRiskCategory { get; set; }
          decimal? HtsRiskScore { get; set; }        
          
    }
}
