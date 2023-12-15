using DwapiCentral.Contracts.Prep;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models.Stage
{
    public class StagePrepAdverseEvent : IPrepAdverseEvent
    {
        public Guid Id { get; set; }
        public string PrepNumber { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string? FacilityName { get; set; }
        public string? AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string? Severity { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? AdverseEventActionTaken { get; set; }
        public string? AdverseEventClinicalOutcome { get; set; }
        public string? AdverseEventIsPregnant { get; set; }
        public string? AdverseEventCause { get; set; }
        public string? AdverseEventRegimen { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
    }
}
