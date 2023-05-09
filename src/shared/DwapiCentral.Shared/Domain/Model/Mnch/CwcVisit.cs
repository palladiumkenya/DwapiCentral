using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Mnch
{
    public class CwcVisit : Entity<Guid>
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool? Processed { get; set; }
        public string QueueId { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public Guid FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string PatientMnchID { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? VisitID { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? MUAC { get; set; }
        public string WeightCategory { get; set; }
        public string Stunted { get; set; }
        public string InfantFeeding { get; set; }
        public string MedicationGiven { get; set; }
        public string TBAssessment { get; set; }
        public string MNPsSupplementation { get; set; }
        public string Immunization { get; set; }
        public string DangerSigns { get; set; }
        public string Milestones { get; set; }
        public string VitaminA { get; set; }
        public string Disability { get; set; }
        public string ReceivedMosquitoNet { get; set; }
        public string Dewormed { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ReferralReasons { get; set; }
        public string FollowUP { get; set; }
        public DateTime? NextAppointment { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
