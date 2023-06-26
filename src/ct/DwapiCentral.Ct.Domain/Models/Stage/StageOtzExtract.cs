using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageOtzExtract : StageExtract, IOtz
    {
        public string? FacilityName { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string? TransferInStatus { get; set; }
        public string? ModulesPreviouslyCovered { get; set; }
        public string? ModulesCompletedToday { get; set; }
        public string? SupportGroupInvolvement { get; set; }
        public string? Remarks { get; set; }
        public string? TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
