using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class OvcSourceDto : SourceDto, IOvc
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string RelationshipToClient { get; set; }
        public string EnrolledinCPIMS { get; set; }
        public string CPIMSUniqueIdentifier { get; set; }
        public string PartnerOfferingOVCServices { get; set; }
        public string OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int PatientPk { get; set; }
        public bool Voided { get; set; }
        public DateTime? Extracted { get; set; }
        Guid IOvc.PatientId { get; set; }
    }
}