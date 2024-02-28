using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class PatientVisitSourceDto :IVisit
    {
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }
        public int? VisitId { get ; set ; }
        public DateTime? VisitDate { get ; set ; }
        public string? Service { get ; set ; }
        public string? VisitType { get ; set ; }
        public int? WHOStage { get ; set ; }
        public string? WABStage { get ; set ; }
        public string? Pregnant { get ; set ; }
        public DateTime? LMP { get ; set ; }
        public DateTime? EDD { get ; set ; }
        public decimal? Height { get ; set ; }
        public decimal? Weight { get ; set ; }
        public string? BP { get ; set ; }
        public string? OI { get ; set ; }
        public DateTime? OIDate { get ; set ; }
        public DateTime? SubstitutionFirstlineRegimenDate { get ; set ; }
        public string? SubstitutionFirstlineRegimenReason { get ; set ; }
        public DateTime? SubstitutionSecondlineRegimenDate { get ; set ; }
        public string? SubstitutionSecondlineRegimenReason { get ; set ; }
        public DateTime? SecondlineRegimenChangeDate { get ; set ; }
        public string? SecondlineRegimenChangeReason { get ; set ; }
        public string? Adherence { get ; set ; }
        public string? AdherenceCategory { get ; set ; }
        public string? FamilyPlanningMethod { get ; set ; }
        public string? PwP { get ; set ; }
        public decimal? GestationAge { get ; set ; }
        public DateTime? NextAppointmentDate { get ; set ; }
        public string? StabilityAssessment { get ; set ; }
        public string? DifferentiatedCare { get ; set ; }
        public string? PopulationType { get ; set ; }
        public string? KeyPopulationType { get ; set ; }
        public string? VisitBy { get ; set ; }
        public decimal? Temp { get ; set ; }
        public int? PulseRate { get ; set ; }
        public int? RespiratoryRate { get ; set ; }
        public decimal? OxygenSaturation { get ; set ; }
        public int? Muac { get ; set ; }
        public string? NutritionalStatus { get ; set ; }
        public string? EverHadMenses { get ; set ; }
        public string? Breastfeeding { get ; set ; }
        public string? Menopausal { get ; set ; }
        public string? NoFPReason { get ; set ; }
        public string? ProphylaxisUsed { get ; set ; }
        public string? CTXAdherence { get ; set ; }
        public string? CurrentRegimen { get ; set ; }
        public string? HCWConcern { get ; set ; }
        public string? TCAReason { get ; set ; }
        public string? ClinicalNotes { get ; set ; }
        public string? GeneralExamination { get ; set ; }
        public string? SystemExamination { get ; set ; }
        public string? Skin { get ; set ; }
        public string? Eyes { get ; set ; }
        public string? ENT { get ; set ; }
        public string? Chest { get ; set ; }
        public string? CVS { get ; set ; }
        public string? Abdomen { get ; set ; }
        public string? CNS { get ; set ; }
        public string? Genitourinary { get ; set ; }
        public DateTime? RefillDate { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
        public string? ZScore { get ; set ; }
        public int? ZScoreAbsolute { get ; set ; }
        public string? PaedsDisclosure { get ; set ; }
        public string? WHOStagingOI { get; set; }
        public int Mhash { get ; set ; }

        public PatientVisitSourceDto()
        {

        }

        public PatientVisitSourceDto(PatientVisitExtract patientVisitExtract)
        {
            VisitId = patientVisitExtract.VisitId;
            VisitDate = patientVisitExtract.VisitDate;
            Service = patientVisitExtract.Service;
            VisitType = patientVisitExtract.VisitType;
            WHOStage = patientVisitExtract.WHOStage;
            WABStage = patientVisitExtract.WABStage;
            Pregnant = patientVisitExtract.Pregnant;
            LMP = patientVisitExtract.LMP;
            EDD = patientVisitExtract.EDD;
            Height = patientVisitExtract.Height;
            Weight = patientVisitExtract.Weight;
            BP = patientVisitExtract.BP;
            OI = patientVisitExtract.OI;
            OIDate = patientVisitExtract.OIDate;
            SubstitutionFirstlineRegimenDate = patientVisitExtract.SubstitutionFirstlineRegimenDate;
            SubstitutionFirstlineRegimenReason = patientVisitExtract.SubstitutionFirstlineRegimenReason;
            SubstitutionSecondlineRegimenDate = patientVisitExtract.SubstitutionSecondlineRegimenDate;
            SubstitutionSecondlineRegimenReason = patientVisitExtract.SubstitutionSecondlineRegimenReason;
            SecondlineRegimenChangeDate = patientVisitExtract.SecondlineRegimenChangeDate;
            SecondlineRegimenChangeReason = patientVisitExtract.SecondlineRegimenChangeReason;
            Adherence = patientVisitExtract.Adherence;
            AdherenceCategory = patientVisitExtract.AdherenceCategory;
            FamilyPlanningMethod = patientVisitExtract.FamilyPlanningMethod;
            PwP = patientVisitExtract.PwP;
            GestationAge = patientVisitExtract.GestationAge;
            NextAppointmentDate = patientVisitExtract.NextAppointmentDate;
            SiteCode = patientVisitExtract.SiteCode;
            WHOStagingOI = patientVisitExtract.WHOStagingOI;

            PatientPk = patientVisitExtract.PatientPk;
            StabilityAssessment = patientVisitExtract.StabilityAssessment;
            DifferentiatedCare = patientVisitExtract.DifferentiatedCare;
            PopulationType = patientVisitExtract.PopulationType;
            KeyPopulationType = patientVisitExtract.KeyPopulationType;
            RefillDate = patientVisitExtract.RefillDate;
            ZScore = patientVisitExtract.ZScore;
            ZScoreAbsolute = patientVisitExtract.ZScoreAbsolute;
            PaedsDisclosure = patientVisitExtract.PaedsDisclosure;
            Date_Created = patientVisitExtract.Date_Created;
            Date_Last_Modified = patientVisitExtract.Date_Last_Modified;
            RecordUUID = patientVisitExtract.RecordUUID;

        }



        public IEnumerable<PatientVisitSourceDto> GeneratePatientVisitExtractDtOs(
            IEnumerable<PatientVisitExtract> extracts)
        {
            var visitExtractDtos = new List<PatientVisitSourceDto>();
            foreach (var e in extracts.ToList())
            {
                visitExtractDtos.Add(new PatientVisitSourceDto(e));
            }

            return visitExtractDtos;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
