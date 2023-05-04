using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
     public interface IHtsTestKits : IEntity
    {
          string FacilityName { get; set; }         
          string HtsNumber { get; set; }         
          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          int? EncounterId { get; set; }
          string TestKitName1 { get; set; }
          string TestKitLotNumber1 { get; set; }
          string TestKitExpiry1 { get; set; }
          string TestResult1 { get; set; }
          string TestKitName2 { get; set; }
          string TestKitLotNumber2 { get; set; }
          string TestKitExpiry2 { get; set; }
          string TestResult2 { get; set; }
          Guid FacilityId { get; set; }
    }
}
