using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class StatusSourceDto : IStatus
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public string? ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? ExitReason { get; set; }
        public string? TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public DateTime? ReEnrollmentDate { get; set; }
        public string? ReasonForDeath { get; set; }
        public string? SpecificDeathReason { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime? EffectiveDiscontinuationDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public StatusSourceDto() { }

        public StatusSourceDto(PatientStatusExtract patientStatusExtract)
        {
            ExitDescription = patientStatusExtract.ExitDescription;
            ExitDate = patientStatusExtract.ExitDate;
            ExitReason = patientStatusExtract.ExitReason;
            SiteCode = patientStatusExtract.SiteCode;
            PatientPk = patientStatusExtract.PatientPk;
           

            TOVerified = patientStatusExtract.TOVerified;
            TOVerifiedDate = patientStatusExtract.TOVerifiedDate;
            ReEnrollmentDate = patientStatusExtract.ReEnrollmentDate;

            ReasonForDeath = patientStatusExtract.ReasonForDeath;
            SpecificDeathReason = patientStatusExtract.SpecificDeathReason;
            DeathDate = patientStatusExtract.DeathDate;
            EffectiveDiscontinuationDate = patientStatusExtract.EffectiveDiscontinuationDate;
            Date_Created = patientStatusExtract.Date_Created;
            Date_Last_Modified = patientStatusExtract.Date_Last_Modified;
            RecordUUID = patientStatusExtract.RecordUUID;

        }



        public IEnumerable<StatusSourceDto> GeneratePatientStatusExtractDtOs(IEnumerable<PatientStatusExtract> extracts)
        {
            var statusExtractDtos = new List<StatusSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new StatusSourceDto(e));
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
