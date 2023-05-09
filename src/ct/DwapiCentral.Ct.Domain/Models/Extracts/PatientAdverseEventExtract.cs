using System;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;


namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class PatientAdverseEventExtract : Entity, IPatientAdverse
    {
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public bool? AdverseEventIsPregnant { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdverseEventRegimen { get; set; }
        public string AdverseEventCause { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public PatientAdverseEventExtract()
        {
            Created = DateTime.Now;
        }

        public PatientAdverseEventExtract(string adverseEvent, DateTime? adverseEventStartDate,
            DateTime? adverseEventEndDate, string severity, string adverseEventClinicalOutcome,
            string adverseEventActionTaken, bool? adverseEventIsPregnant, DateTime? visitDate,
            string adverseEventRegimen, string adverseEventCause, Guid patientId,
            string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            AdverseEvent = adverseEvent;
            AdverseEventStartDate = adverseEventStartDate;
            AdverseEventEndDate = adverseEventEndDate;
            Severity = severity;
            AdverseEventClinicalOutcome = adverseEventClinicalOutcome;
            AdverseEventActionTaken = adverseEventActionTaken;
            AdverseEventIsPregnant = adverseEventIsPregnant;
            VisitDate = visitDate;
            AdverseEventRegimen = adverseEventRegimen;
            AdverseEventCause = adverseEventCause;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}