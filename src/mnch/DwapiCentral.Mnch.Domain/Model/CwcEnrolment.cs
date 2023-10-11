using DwapiCentral.Contracts.Mnch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model
{
    public class CwcEnrolment : ICwcEnrolment
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string? RecordUUID { get; set; }
        public string? PatientIDCWC { get; set; }
        public string? HEIID { get; set; }
        public string? MothersPkv { get; set; }
        public string? Pkv { get; set; }
        public DateTime? RegistrationAtCWC { get; set; }
        public DateTime? RegistrationAtHEI { get; set; }
        public int? VisitID { get; set; }
        public DateTime? Gestation { get; set; }
        public string? BirthWeight { get; set; }
        public decimal? BirthLength { get; set; }
        public int? BirthOrder { get; set; }
        public string? BirthType { get; set; }
        public string? PlaceOfDelivery { get; set; }
        public string? ModeOfDelivery { get; set; }
        public string? SpecialNeeds { get; set; }
        public string? SpecialCare { get; set; }
        public string? HEI { get; set; }
        public string? MotherAlive { get; set; }
        public string? MothersCCCNo { get; set; }
        public string? TransferIn { get; set; }
        public string? TransferInDate { get; set; }
        public string? TransferredFrom { get; set; }
        public string? HEIDate { get; set; }
        public string? NVP { get; set; }
        public string? BreastFeeding { get; set; }
        public string? ReferredFrom { get; set; }
        public string? ARTMother { get; set; }
        public string? ARTRegimenMother { get; set; }
        public DateTime? ARTStartDateMother { get; set; }
        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
