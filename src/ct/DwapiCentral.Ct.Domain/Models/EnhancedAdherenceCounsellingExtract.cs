using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System.ComponentModel.DataAnnotations;

namespace DwapiCentral.Ct.Domain.Models
{
    public class EnhancedAdherenceCounsellingExtract : IEnhancedAdherenceCounselling
    {
        [Key]
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public int? SessionNumber { get; set; }
        public DateTime? DateOfFirstSession { get; set; }
        public int? PillCountAdherence { get; set; }
        public string? MMAS4_1 { get; set; }
        public string? MMAS4_2 { get; set; }
        public string? MMAS4_3 { get; set; }
        public string? MMAS4_4 { get; set; }
        public string? MMSA8_1 { get; set; }
        public string? MMSA8_2 { get; set; }
        public string? MMSA8_3 { get; set; }
        public string? MMSA8_4 { get; set; }
        public string? MMSAScore { get; set; }
        public string? EACRecievedVL { get; set; }
        public string? EACVL { get; set; }
        public string? EACVLConcerns { get; set; }
        public string? EACVLThoughts { get; set; }
        public string? EACWayForward { get; set; }
        public string? EACCognitiveBarrier { get; set; }
        public string? EACBehaviouralBarrier_1 { get; set; }
        public string? EACBehaviouralBarrier_2 { get; set; }
        public string? EACBehaviouralBarrier_3 { get; set; }
        public string? EACBehaviouralBarrier_4 { get; set; }
        public string? EACBehaviouralBarrier_5 { get; set; }
        public string? EACEmotionalBarriers_1 { get; set; }
        public string? EACEmotionalBarriers_2 { get; set; }
        public string? EACEconBarrier_1 { get; set; }
        public string? EACEconBarrier_2 { get; set; }
        public string? EACEconBarrier_3 { get; set; }
        public string? EACEconBarrier_4 { get; set; }
        public string? EACEconBarrier_5 { get; set; }
        public string? EACEconBarrier_6 { get; set; }
        public string? EACEconBarrier_7 { get; set; }
        public string? EACEconBarrier_8 { get; set; }
        public string? EACReviewImprovement { get; set; }
        public string? EACReviewMissedDoses { get; set; }
        public string? EACReviewStrategy { get; set; }
        public string? EACReferral { get; set; }
        public string? EACReferralApp { get; set; }
        public string? EACReferralExperience { get; set; }
        public string? EACHomevisit { get; set; }
        public string? EACAdherencePlan { get; set; }
        public DateTime? EACFollowupDate { get; set; }               
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
