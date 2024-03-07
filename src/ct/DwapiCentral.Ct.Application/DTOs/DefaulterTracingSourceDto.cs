using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class DefaulterTracingSourceDto : IDefaulterTracing
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public int? EncounterId { get; set; }
        public string? TracingType { get; set; }
        public string? TracingOutcome { get; set; }
        public int? AttemptNumber { get; set; }
        public string? IsFinalTrace { get; set; }
        public string? TrueStatus { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? Comments { get; set; }
        public DateTime? BookingDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
        public DateTime? DatePromisedToCome { get; set; }
        public string? ReasonForMissedAppointment { get ; set ; }
        public DateTime? DateOfMissedAppointment { get; set ; }

        public DefaulterTracingSourceDto()
        {
        }

        public DefaulterTracingSourceDto(DefaulterTracingExtract DefaulterTracingExtract)
        {
            FacilityName = DefaulterTracingExtract.FacilityName;
            VisitID = DefaulterTracingExtract.VisitID;
            VisitDate = DefaulterTracingExtract.VisitDate;
            EncounterId = DefaulterTracingExtract.EncounterId;
            TracingType = DefaulterTracingExtract.TracingType;
            TracingOutcome = DefaulterTracingExtract.TracingOutcome;
            AttemptNumber = DefaulterTracingExtract.AttemptNumber;
            IsFinalTrace = DefaulterTracingExtract.IsFinalTrace;
            TrueStatus = DefaulterTracingExtract.TrueStatus;
            CauseOfDeath = DefaulterTracingExtract.CauseOfDeath;
            Comments = DefaulterTracingExtract.Comments;
            BookingDate = DefaulterTracingExtract.BookingDate;
            SiteCode = DefaulterTracingExtract.SiteCode;
            PatientPk = DefaulterTracingExtract.PatientPk;
           
            Date_Created = DefaulterTracingExtract.Date_Created;
            Date_Last_Modified = DefaulterTracingExtract.Date_Last_Modified;
            RecordUUID = DefaulterTracingExtract.RecordUUID;

        }

        public IEnumerable<DefaulterTracingSourceDto> GenerateDefaulterTracingExtractDtOs(IEnumerable<DefaulterTracingExtract> extracts)
        {
            var statusExtractDtos = new List<DefaulterTracingSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DefaulterTracingSourceDto(e));
            }
            return statusExtractDtos;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}