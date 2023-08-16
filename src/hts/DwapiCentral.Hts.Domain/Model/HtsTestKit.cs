using DwapiCentral.Contracts.Hts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model
{
    public class HtsTestKit : IHtsTestKits
    {
        [Key]
        public Guid Id { get; set; }
        public string HtsNumber { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? EncounterId { get; set; }
        public string FacilityName { get; set; }
        public string? TestKitName1 { get; set; }
        public string? TestKitLotNumber1 { get; set; }
        public string? TestKitExpiry1 { get; set; }
        public string? TestResult1 { get; set; }
        public string? TestKitName2 { get; set; }
        public string? TestKitLotNumber2 { get; set; }
        public string? TestKitExpiry2 { get; set; }
        public string? TestResult2 { get; set; }
        public string? SyphilisResult { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
