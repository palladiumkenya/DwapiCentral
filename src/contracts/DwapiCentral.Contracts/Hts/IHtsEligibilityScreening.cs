using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public interface IHtsEligibilityScreening : IExtract
    {
         string HtsNumber { get; set; }
         string EncounterId { get; set; }
         int? VisitID { get; set; }
         DateTime? VisitDate { get; set; }
         string? PopulationType { get; set; }
         string? KeyPopulation { get; set; }
         string? PriorityPopulation { get; set; }
         string? Department { get; set; }
         string? PatientType { get; set; }
         string? IsHealthWorker { get; set; }
         string? RelationshipWithContact { get; set; }
         string? TestedHIVBefore { get; set; }
         string? WhoPerformedTest { get; set; }
         string? ResultOfHIV { get; set; }
         string? StartedOnART { get; set; }
         string? CCCNumber { get; set; }
         string? EverHadSex { get; set; }
         string? SexuallyActive { get; set; }
         string? NewPartner { get; set; }
         string? PartnerHIVStatus { get; set; }
         string? CoupleDiscordant { get; set; }
         string? MultiplePartners { get; set; }
         int? NumberOfPartners { get; set; }
         string? AlcoholSex { get; set; }
         string? MoneySex { get; set; }
         string? CondomBurst { get; set; }
         string? UnknownStatusPartner { get; set; }
         string? KnownStatusPartner { get; set; }
         string? Pregnant { get; set; }
         string? BreastfeedingMother { get; set; }
         string? ExperiencedGBV { get; set; }

         string? EverOnPrep { get; set; }
         string? CurrentlyOnPrep { get; set; }
         string? EverOnPep { get; set; }
         string? CurrentlyOnPep { get; set; }
         string? EverHadSTI { get; set; }
         string? CurrentlyHasSTI { get; set; }
         string? EverHadTB { get; set; }
         string? SharedNeedle { get; set; }
         string? NeedleStickInjuries { get; set; }
         string? TraditionalProcedures { get; set; }
         string? ChildReasonsForIneligibility { get; set; }
         string? EligibleForTest { get; set; }
         string? ReasonsForIneligibility { get; set; }
         int? SpecificReasonForIneligibility { get; set; }

         string? MothersStatus { get; set; }
         DateTime? DateTestedSelf { get; set; }
         string? ResultOfHIVSelf { get; set; }
         DateTime? DateTestedProvider { get; set; }
         string? ScreenedTB { get; set; }
         string? Cough { get; set; }
         string? Fever { get; set; }
         string? WeightLoss { get; set; }
         string? NightSweats { get; set; }
         string? Lethargy { get; set; }
         string? TBStatus { get; set; }
         string? ReferredForTesting { get; set; }

         string? AssessmentOutcome { get; set; }
         string? TypeGBV { get; set; }
         string? ForcedSex { get; set; }
         string? ReceivedServices { get; set; }
         string? ContactWithTBCase { get; set; }

         string? Disability { get; set; }
         string? DisabilityType { get; set; }
         string? HTSStrategy { get; set; }
         string? HTSEntryPoint { get; set; }
         string? HIVRiskCategory { get; set; }
         string? ReasonRefferredForTesting { get; set; }
         string? ReasonNotReffered { get; set; }
         string? HtsRiskScore { get; set; }

    }
}
