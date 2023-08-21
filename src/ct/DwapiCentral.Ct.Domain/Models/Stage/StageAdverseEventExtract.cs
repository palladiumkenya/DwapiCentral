using DwapiCentral.Contracts.Common;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageAdverseEventExtract : StageExtract,  IPatientAdverse
    {
        public DateTime VisitDate { get; set; }
        public string? AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string? Severity { get; set; }
        public string? AdverseEventClinicalOutcome { get; set; }
        public string? AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public string? AdverseEventRegimen { get; set; }
        public string? AdverseEventCause { get; set; }
        public int PatientPk { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public int SiteCode { get; set ; }
        public bool? Voided { get ; set; }
    }
}
