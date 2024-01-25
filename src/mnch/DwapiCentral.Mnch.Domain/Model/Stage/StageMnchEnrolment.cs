using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model.Stage
{
    public class StageMnchEnrolment : IMnchEnrolment
    {
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string PatientMnchID { get; set; }
        public string? FacilityName { get; set; }
        public string? ServiceType { get; set; }
        public DateTime? EnrollmentDateAtMnch { get; set; }
        public DateTime? MnchNumber { get; set; }
        public DateTime? FirstVisitAnc { get; set; }
        public string? Parity { get; set; }
        public int? Gravidae { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDDFromLMP { get; set; }
        public string? HIVStatusBeforeANC { get; set; }
        public DateTime? HIVTestDate { get; set; }
        public string? PartnerHIVStatus { get; set; }
        public DateTime? PartnerHIVTestDate { get; set; }
        public string? BloodGroup { get; set; }
        public string? StatusAtMnch { get; set; }
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
