using DwapiCentral.Ct.Application.Interfaces.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.DTOs.Extract
{
    public class PatientStatusExtractDTO : IPatientStatusExtractDTO
    {
        public string ExitDescription { get; set; }
        public DateTime? ExitDate { get; set; }
        public string ExitReason { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public string TOVerified { get; set; }
        public DateTime? TOVerifiedDate { get; set; }
        public DateTime? ReEnrollmentDate { get; set; }
        public string ReasonForDeath { get; set; }
        public string SpecificDeathReason { get; set; }
        public DateTime? DeathDate { get; set; }
        public DateTime? EffectiveDiscontinuationDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? Created { get; set; }
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public bool Voided { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public PatientStatusExtractDTO()
        {
        }

        public PatientStatusExtractDTO(string exitDescription, DateTime? exitDate, string exitReason, string emr, string project, Guid patientId, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            ExitDescription = exitDescription;
            ExitDate = exitDate;
            ExitReason = exitReason;
            Emr = emr;
            Project = project;
            PatientId = patientId;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }

        public PatientStatusExtractDTO(PatientStatusExtract patientStatusExtract)
        {
            ExitDescription = patientStatusExtract.ExitDescription;
            ExitDate = patientStatusExtract.ExitDate;
            ExitReason = patientStatusExtract.ExitReason;
            Emr = patientStatusExtract.Emr;
            Project = patientStatusExtract.Project;
            PatientId = patientStatusExtract.PatientId;

            TOVerified = patientStatusExtract.TOVerified;
            TOVerifiedDate = patientStatusExtract.TOVerifiedDate;
            ReEnrollmentDate = patientStatusExtract.ReEnrollmentDate;

            ReasonForDeath = patientStatusExtract.ReasonForDeath;
            SpecificDeathReason = patientStatusExtract.SpecificDeathReason;
            DeathDate = patientStatusExtract.DeathDate;
            EffectiveDiscontinuationDate = patientStatusExtract.EffectiveDiscontinuationDate;
            Date_Created = patientStatusExtract.Date_Created;
            Date_Last_Modified = patientStatusExtract.Date_Last_Modified;
        }



        public IEnumerable<PatientStatusExtractDTO> GeneratePatientStatusExtractDtOs(IEnumerable<PatientStatusExtract> extracts)
        {
            var statusExtractDtos = new List<PatientStatusExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new PatientStatusExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public PatientStatusExtract GeneratePatientStatusExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientStatusExtract(ExitDescription, ExitDate, ExitReason, PatientId, Emr, Project, TOVerified, TOVerifiedDate, ReEnrollmentDate, ReasonForDeath, SpecificDeathReason, DeathDate, EffectiveDiscontinuationDate, Date_Created, Date_Last_Modified);
        }
    }
}
