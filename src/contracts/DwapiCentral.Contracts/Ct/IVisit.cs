using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public interface IVisit : IExtract
    {
        Guid Id { get; set; }
        int? VisitId { get; set; }
        DateTime? VisitDate { get; set; }
        string Service { get; set; }
        string VisitType { get; set; }
        int? WHOStage { get; set; }
        string WABStage { get; set; }
        string Pregnant { get; set; }
        DateTime? LMP { get; set; }
        DateTime? EDD { get; set; }
        decimal? Height { get; set; }
        decimal? Weight { get; set; }
        string BP { get; set; }
        string OI { get; set; }
        DateTime? OIDate { get; set; }
        DateTime? SubstitutionFirstlineRegimenDate { get; set; }
        string SubstitutionFirstlineRegimenReason { get; set; }
        DateTime? SubstitutionSecondlineRegimenDate { get; set; }
        string SubstitutionSecondlineRegimenReason { get; set; }
        DateTime? SecondlineRegimenChangeDate { get; set; }
        string SecondlineRegimenChangeReason { get; set; }
        string Adherence { get; set; }
        string AdherenceCategory { get; set; }
        string FamilyPlanningMethod { get; set; }
        string PwP { get; set; }
        decimal? GestationAge { get; set; }
        DateTime? NextAppointmentDate { get; set; }
        string StabilityAssessment { get; set; }
        string DifferentiatedCare { get; set; }
        string PopulationType { get; set; }
        string KeyPopulationType { get; set; }
        Guid PatientId { get; set; }
        

        string VisitBy { get; set; }
        decimal? Temp { get; set; }
        int? PulseRate { get; set; }
        int? RespiratoryRate { get; set; }
        decimal? OxygenSaturation { get; set; }
        int? Muac { get; set; }
        string NutritionalStatus { get; set; }
        string EverHadMenses { get; set; }
        string Breastfeeding { get; set; }
        string Menopausal { get; set; }
        string NoFPReason { get; set; }
        string ProphylaxisUsed { get; set; }
        string CTXAdherence { get; set; }
        string CurrentRegimen { get; set; }
        string HCWConcern { get; set; }
        string TCAReason { get; set; }
        string ClinicalNotes { get; set; }

        string GeneralExamination { get; set; }
        string SystemExamination { get; set; }
        string Skin { get; set; }
        string Eyes { get; set; }
        string ENT { get; set; }
        string Chest { get; set; }
        string CVS { get; set; }
        string Abdomen { get; set; }
        string CNS { get; set; }
        string Genitourinary { get; set; }
        DateTime? RefillDate { get; set; }

      
    }
}
