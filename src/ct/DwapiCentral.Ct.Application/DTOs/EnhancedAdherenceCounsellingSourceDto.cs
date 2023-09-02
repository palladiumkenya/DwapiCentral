using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class EnhancedAdherenceCounselingSourceDto : IEnhancedAdherenceCounselling
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
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
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }


        public EnhancedAdherenceCounselingSourceDto()
        {
        }

        public EnhancedAdherenceCounselingSourceDto(EnhancedAdherenceCounsellingExtract EnhancedAdherenceCounsellingExtract)
        {
            FacilityName = EnhancedAdherenceCounsellingExtract.FacilityName;
            VisitID = EnhancedAdherenceCounsellingExtract.VisitID;
            VisitDate = EnhancedAdherenceCounsellingExtract.VisitDate;
            SessionNumber = EnhancedAdherenceCounsellingExtract.SessionNumber;
            DateOfFirstSession = EnhancedAdherenceCounsellingExtract.DateOfFirstSession;
            PillCountAdherence = EnhancedAdherenceCounsellingExtract.PillCountAdherence;
            MMAS4_1 = EnhancedAdherenceCounsellingExtract.MMAS4_1;
            MMAS4_2 = EnhancedAdherenceCounsellingExtract.MMAS4_2;
            MMAS4_3 = EnhancedAdherenceCounsellingExtract.MMAS4_3;
            MMAS4_4 = EnhancedAdherenceCounsellingExtract.MMAS4_4;
            MMSA8_1 = EnhancedAdherenceCounsellingExtract.MMSA8_1;
            MMSA8_2 = EnhancedAdherenceCounsellingExtract.MMSA8_2;
            MMSA8_3 = EnhancedAdherenceCounsellingExtract.MMSA8_3;
            MMSA8_4 = EnhancedAdherenceCounsellingExtract.MMSA8_4;
            MMSAScore = EnhancedAdherenceCounsellingExtract.MMSAScore;
            EACRecievedVL = EnhancedAdherenceCounsellingExtract.EACRecievedVL;
            EACVL = EnhancedAdherenceCounsellingExtract.EACVL;
            EACVLConcerns = EnhancedAdherenceCounsellingExtract.EACVLConcerns;
            EACVLThoughts = EnhancedAdherenceCounsellingExtract.EACVLThoughts;
            EACWayForward = EnhancedAdherenceCounsellingExtract.EACWayForward;
            EACCognitiveBarrier = EnhancedAdherenceCounsellingExtract.EACCognitiveBarrier;
            EACBehaviouralBarrier_1 = EnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_1;
            EACBehaviouralBarrier_2 = EnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_2;
            EACBehaviouralBarrier_3 = EnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_3;
            EACBehaviouralBarrier_4 = EnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_4;
            EACBehaviouralBarrier_5 = EnhancedAdherenceCounsellingExtract.EACBehaviouralBarrier_5;
            EACEmotionalBarriers_1 = EnhancedAdherenceCounsellingExtract.EACEmotionalBarriers_1;
            EACEmotionalBarriers_2 = EnhancedAdherenceCounsellingExtract.EACEmotionalBarriers_2;
            EACEconBarrier_1 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_1;
            EACEconBarrier_2 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_2;
            EACEconBarrier_3 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_3;
            EACEconBarrier_4 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_4;
            EACEconBarrier_5 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_5;
            EACEconBarrier_6 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_6;
            EACEconBarrier_7 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_7;
            EACEconBarrier_8 = EnhancedAdherenceCounsellingExtract.EACEconBarrier_8;
            EACReviewImprovement = EnhancedAdherenceCounsellingExtract.EACReviewImprovement;
            EACReviewMissedDoses = EnhancedAdherenceCounsellingExtract.EACReviewMissedDoses;
            EACReviewStrategy = EnhancedAdherenceCounsellingExtract.EACReviewStrategy;
            EACReferral = EnhancedAdherenceCounsellingExtract.EACReferral;
            EACReferralApp = EnhancedAdherenceCounsellingExtract.EACReferralApp;
            EACReferralExperience = EnhancedAdherenceCounsellingExtract.EACReferralExperience;
            EACHomevisit = EnhancedAdherenceCounsellingExtract.EACHomevisit;
            EACAdherencePlan = EnhancedAdherenceCounsellingExtract.EACAdherencePlan;
            EACFollowupDate = EnhancedAdherenceCounsellingExtract.EACFollowupDate;

            PatientPk = EnhancedAdherenceCounsellingExtract.PatientPk;
            SiteCode = EnhancedAdherenceCounsellingExtract.SiteCode;
           
            Date_Created = EnhancedAdherenceCounsellingExtract.Date_Created;
            Date_Last_Modified = EnhancedAdherenceCounsellingExtract.Date_Last_Modified;
            RecordUUID = EnhancedAdherenceCounsellingExtract.RecordUUID;

        }



        public IEnumerable<EnhancedAdherenceCounselingSourceDto> GenerateEnhancedAdherenceCounsellingExtractDtOs(IEnumerable<EnhancedAdherenceCounsellingExtract> extracts)
        {
            var statusExtractDtos = new List<EnhancedAdherenceCounselingSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new EnhancedAdherenceCounselingSourceDto(e));
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