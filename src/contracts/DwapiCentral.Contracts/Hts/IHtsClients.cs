using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
      public interface IHtsClients : IEntity
    {
          string HtsNumber { get; set; }
      


          int PatientPk { get; set; }
          int SiteCode { get; set; }
          string FacilityName { get; set; }
          string Serial { get; set; }
         
          DateTime? StatusDate { get; set; }
          

          int? EncounterId { get; set; }
          DateTime? VisitDate { get; set; }
          DateTime? Dob { get; set; }
          string Gender { get; set; }
          string MaritalStatus { get; set; }
          string KeyPop { get; set; }
          string TestedBefore { get; set; }
          int? MonthsLastTested { get; set; }
          string ClientTestedAs { get; set; }
          string StrategyHTS { get; set; }
          string TestKitName1 { get; set; }
          string TestKitLotNumber1 { get; set; }
          DateTime? TestKitExpiryDate1 { get; set; }
          string TestResultsHTS1 { get; set; }
          string TestKitName2 { get; set; }
          string TestKitLotNumber2 { get; set; }
          string TestKitExpiryDate2 { get; set; }
          string TestResultsHTS2 { get; set; }
          string FinalResultHTS { get; set; }
          string FinalResultsGiven { get; set; }
          string TBScreeningHTS { get; set; }
          string ClientSelfTested { get; set; }
          string CoupleDiscordant { get; set; }
          string TestType { get; set; }

          string KeyPopulationType { get; set; }
          string PopulationType { get; set; }
          string PatientDisabled { get; set; }
          string DisabilityType { get; set; }
          string PatientConsented { get; set; }
          string County { get; set; }
          string SubCounty { get; set; }
          string Ward { get; set; }
          string NUPI { get; set; }
          string Pkv { get; set; }

          Guid FacilityId { get; set; }
    }
}
