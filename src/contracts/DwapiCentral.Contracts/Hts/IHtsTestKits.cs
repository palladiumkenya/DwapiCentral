using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
     public interface IHtsTestKits : IExtract
    {
          string HtsNumber { get; set; }
          string FacilityName { get; set; } 
          int? EncounterId { get; set; }
          string? TestKitName1 { get; set; }
          string? TestKitLotNumber1 { get; set; }
          string? TestKitExpiry1 { get; set; }
          string? TestResult1 { get; set; }
          string? TestKitName2 { get; set; }
          string? TestKitLotNumber2 { get; set; }
          string? TestKitExpiry2 { get; set; }
          string? TestResult2 { get; set; }
          string? SyphilisResult { get; set; }        
          DateTime? Date_Last_Modified { get; set; }
    }
}
