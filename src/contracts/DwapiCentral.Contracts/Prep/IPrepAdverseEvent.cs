using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
    public interface IPrepAdverseEvent : IEntity
    {
    
        public bool? Processed { get; set; }
        public string QueueId { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public Guid FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string PrepNumber { get; set; }
        public string AdverseEvent { get; set; }
        public DateTime? AdverseEventStartDate { get; set; }
        public DateTime? AdverseEventEndDate { get; set; }
        public string Severity { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AdverseEventActionTaken { get; set; }
        public string AdverseEventClinicalOutcome { get; set; }
        public string AdverseEventIsPregnant { get; set; }
        public string AdverseEventCause { get; set; }
        public string AdverseEventRegimen { get; set; }
       

    }
}
