using DwapiCentral.Ct.Application.Interfaces.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DwapiCentral.Ct.Application.DTOs.Extract
{
    public class DefaulterTracingExtractDTO : IDefaulterTracingExtractDTO
    {
        public string Emr { get; set; }
        public string Project { get; set; }
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
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public bool Voided { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public DefaulterTracingExtractDTO()
        {
        }

        public DefaulterTracingExtractDTO(DefaulterTracingExtract DefaulterTracingExtract)
        {
            FacilityName =DefaulterTracingExtract.FacilityName;
            VisitID =DefaulterTracingExtract.VisitID;
            VisitDate =DefaulterTracingExtract.VisitDate;
            EncounterId =DefaulterTracingExtract.EncounterId;
            TracingType =DefaulterTracingExtract.TracingType;
            TracingOutcome =DefaulterTracingExtract.TracingOutcome;
            AttemptNumber =DefaulterTracingExtract.AttemptNumber;
            IsFinalTrace =DefaulterTracingExtract.IsFinalTrace;
            TrueStatus =DefaulterTracingExtract.TrueStatus;
            CauseOfDeath =DefaulterTracingExtract.CauseOfDeath;
            Comments =DefaulterTracingExtract.Comments;
            BookingDate =DefaulterTracingExtract.BookingDate;
            PatientId =DefaulterTracingExtract.PatientId;
            Emr =DefaulterTracingExtract.Emr;
            Project =DefaulterTracingExtract.Project;
            Date_Created=DefaulterTracingExtract.Date_Created;
            Date_Last_Modified=DefaulterTracingExtract.Date_Last_Modified;
        }

        public IEnumerable<DefaulterTracingExtractDTO> GenerateDefaulterTracingExtractDtOs(IEnumerable<DefaulterTracingExtract> extracts)
        {
            var statusExtractDtos = new List<DefaulterTracingExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DefaulterTracingExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public DefaulterTracingExtract GenerateDefaulterTracingExtract(Guid patientId)
        {
            PatientId = patientId;
            return new DefaulterTracingExtract(
                FacilityName,
                VisitID,
                VisitDate,
                EncounterId,
                TracingType,
                TracingOutcome,
                AttemptNumber,
                IsFinalTrace,
                TrueStatus,
                CauseOfDeath,
                Comments,
                BookingDate,
                PatientId, Emr, Project,
                Date_Created,
                Date_Last_Modified
            );
        }


    }
}
