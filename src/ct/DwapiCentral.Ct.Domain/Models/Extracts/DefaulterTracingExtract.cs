using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class DefaulterTracingExtract : Entity, IDefaulterTracing
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? EncounterId { get; set; }
        public string TracingType { get; set; }
        public string TracingOutcome { get; set; }
        public int? AttemptNumber { get; set; }
        public string IsFinalTrace { get; set; }
        public string TrueStatus { get; set; }
        public string CauseOfDeath { get; set; }
        public string Comments { get; set; }
        public DateTime? BookingDate { get; set; }
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

        public DefaulterTracingExtract()
        {
            Created = DateTime.Now;
        }

        public DefaulterTracingExtract(string facilityName, int? visitId, DateTime? visitDate, int? encounterId, string tracingType, string tracingOutcome, int? attemptNumber, string isFinalTrace, string trueStatus, string causeOfDeath, string comments, DateTime? bookingDate, Guid patientId,
            string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            EncounterId = encounterId;
            TracingType = tracingType;
            TracingOutcome = tracingOutcome;
            AttemptNumber = attemptNumber;
            IsFinalTrace = isFinalTrace;
            TrueStatus = trueStatus;
            CauseOfDeath = causeOfDeath;
            Comments = comments;
            BookingDate = bookingDate;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
        }
    }
}
