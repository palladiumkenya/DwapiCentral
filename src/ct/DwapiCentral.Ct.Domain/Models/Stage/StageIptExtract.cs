using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageIptExtract : StageExtract, IIpt
    {
        public int VisitID { get ; set ; }
        public DateTime VisitDate { get ; set ; }
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
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? DateCreated { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
