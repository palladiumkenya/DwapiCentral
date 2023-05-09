using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class OtzExtract : Entity, IOtz
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string TransferInStatus { get; set; }
        public string ModulesPreviouslyCovered { get; set; }
        public string ModulesCompletedToday { get; set; }
        public string SupportGroupInvolvement { get; set; }
        public string Remarks { get; set; }
        public string TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
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

        public OtzExtract()
        {
            Created = DateTime.Now;
        }

        public OtzExtract(string facilityName, int? visitId, DateTime? visitDate, DateTime? otzEnrollmentDate, string transferInStatus, string modulesPreviouslyCovered, string modulesCompletedToday, string supportGroupInvolvement, string remarks, string transitionAttritionReason, DateTime? outcomeDate,
            Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            OTZEnrollmentDate = otzEnrollmentDate;
            TransferInStatus = transferInStatus;
            ModulesPreviouslyCovered = modulesPreviouslyCovered;
            ModulesCompletedToday = modulesCompletedToday;
            SupportGroupInvolvement = supportGroupInvolvement;
            Remarks = remarks;
            TransitionAttritionReason = transitionAttritionReason;
            OutcomeDate = outcomeDate;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}
