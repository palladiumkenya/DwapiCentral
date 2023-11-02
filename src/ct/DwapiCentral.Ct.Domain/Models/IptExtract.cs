using System;
using System.ComponentModel.DataAnnotations;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models
{
    public class IptExtract : IIpt
    {
        [Key]
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? VisitID { get ; set ; }
        public DateTime? VisitDate { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string? OnTBDrugs { get ; set ; }
        public string? OnIPT { get ; set ; }
        public string? EverOnIPT { get ; set ; }
        public string? Cough { get ; set ; }
        public string? Fever { get ; set ; }
        public string? NoticeableWeightLoss { get ; set ; }
        public string? NightSweats { get ; set ; }
        public string? Lethargy { get ; set ; }
        public string? ICFActionTaken { get ; set ; }
        public string? TestResult { get ; set ; }
        public string? TBClinicalDiagnosis { get ; set ; }
        public string? ContactsInvited { get ; set ; }
        public string? EvaluatedForIPT { get ; set ; }
        public string? StartAntiTBs { get ; set ; }
        public DateTime? TBRxStartDate { get ; set ; }
        public string? TBScreening { get ; set ; }
        public string? IPTClientWorkUp { get ; set ; }
        public string? StartIPT { get ; set ; }
        public string? IndicationForIPT { get ; set ; }           
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
