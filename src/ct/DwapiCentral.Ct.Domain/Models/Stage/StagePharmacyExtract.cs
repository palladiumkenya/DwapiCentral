using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StagePharmacyExtract : StageExtract, IPharmacy
    {
        public ulong Mhash { get; set; }
        public int? VisitID { get ; set ; }
        public string? Drug { get ; set ; }
        public string? Provider { get ; set ; }
        public DateTime? DispenseDate { get ; set ; }
        public decimal? Duration { get ; set ; }
        public DateTime? ExpectedReturn { get ; set ; }
        public string? TreatmentType { get ; set ; }
        public string? RegimenLine { get ; set ; }
        public string? PeriodTaken { get ; set ; }
        public string? ProphylaxisType { get ; set ; }
        public string? RegimenChangedSwitched { get ; set ; }
        public string? RegimenChangeSwitchReason { get ; set ; }
        public string? StopRegimenReason { get ; set ; }
        public DateTime? StopRegimenDate { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } 
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
       
    }
}
