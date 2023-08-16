using DwapiCentral.Contracts.Hts;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model.Stage
{
    public class StageHtsClientTest : IHtsClientTests
    {
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string HtsNumber { get; set; }
        public int EncounterId { get; set; }
        public DateTime? TestDate { get; set; }
        public string FacilityName { get; set; }
        public bool? Processed { get; set; }
        public string? Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }        
        public string? EverTestedForHiv { get; set; }
        public int? MonthsSinceLastTest { get; set; }
        public string? ClientTestedAs { get; set; }
        public string? EntryPoint { get; set; }
        public string? TestStrategy { get; set; }
        public string? TestResult1 { get; set; }
        public string? TestResult2 { get; set; }
        public string FinalTestResult { get; set; }
        public string? PatientGivenResult { get; set; }
        public string? TbScreening { get; set; }
        public string? ClientSelfTested { get; set; }
        public string? CoupleDiscordant { get; set; }
        public string? TestType { get; set; }
        public string? Consent { get; set; }
        public string? Setting { get; set; }
        public string? Approach { get; set; }
        public string? HtsRiskCategory { get; set; }
        public decimal? HtsRiskScore { get; set; }
        public DateTime? Date_Last_Modified { get; set; }       
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
       
        public Guid? ManifestId { get; set; }
        public LiveStage LiveStage { get; set; }
    }
}
