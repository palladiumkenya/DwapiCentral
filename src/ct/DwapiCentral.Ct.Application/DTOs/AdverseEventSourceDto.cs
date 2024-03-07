using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class AdverseEventSourceDto : IPatientAdverse
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public DateTime? VisitDate { get; set; }
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
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public AdverseEventSourceDto()
        {
        }


        public AdverseEventSourceDto(PatientAdverseEventExtract patientStatusExtract)
        {

            AdverseEvent = patientStatusExtract.AdverseEvent;
            AdverseEventStartDate = patientStatusExtract.AdverseEventStartDate;
            AdverseEventEndDate = patientStatusExtract.AdverseEventEndDate;
            Severity = patientStatusExtract.Severity;
            AdverseEventClinicalOutcome = patientStatusExtract.AdverseEventClinicalOutcome;
            AdverseEventActionTaken = patientStatusExtract.AdverseEventActionTaken;
            AdverseEventIsPregnant = patientStatusExtract.AdverseEventIsPregnant;
            VisitDate = patientStatusExtract.VisitDate;
            AdverseEventRegimen = patientStatusExtract.AdverseEventRegimen;
            AdverseEventCause = patientStatusExtract.AdverseEventCause;
            PatientPk = patientStatusExtract.PatientPk;
            SiteCode = patientStatusExtract.SiteCode;
           
            Date_Created = patientStatusExtract.Date_Created;
            Date_Last_Modified = patientStatusExtract.Date_Last_Modified;
            RecordUUID = patientStatusExtract.RecordUUID;

        }



        public IEnumerable<AdverseEventSourceDto> GeneratePatientAdverseEventExtractDtOs(
            IEnumerable<PatientAdverseEventExtract> extracts)
        {
            var statusExtractDtos = new List<AdverseEventSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new AdverseEventSourceDto(e));
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
