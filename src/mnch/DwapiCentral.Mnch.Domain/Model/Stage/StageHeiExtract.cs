using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model.Stage
{
    public class StageHeiExtract : IHei
    {
        public Guid Id { get; set; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public string RecordUUID { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string PatientMnchID { get ; set ; }
        public DateTime? DNAPCR1Date { get ; set ; }
        public DateTime? DNAPCR2Date { get ; set ; }
        public DateTime? DNAPCR3Date { get ; set ; }
        public DateTime? ConfirmatoryPCRDate { get ; set ; }
        public DateTime? BasellineVLDate { get ; set ; }
        public DateTime? FinalyAntibodyDate { get ; set ; }
        public string? DNAPCR1 { get ; set ; }
        public string? DNAPCR2 { get ; set ; }
        public string? DNAPCR3 { get ; set ; }
        public string? ConfirmatoryPCR { get ; set ; }
        public string? BasellineVL { get ; set ; }
        public string? FinalyAntibody { get ; set ; }
        public DateTime? HEIExitDate { get ; set ; }
        public string? HEIHIVStatus { get ; set ; }
        public string? HEIExitCritearia { get ; set ; }
        public string? PatientHeiId { get ; set ; }
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
