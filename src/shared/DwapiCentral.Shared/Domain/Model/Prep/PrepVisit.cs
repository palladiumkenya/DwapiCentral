using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Prep
{
    public class PrepVisit : Entity<Guid>
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
        public string PrepNumber { get; set; }
        public string HtsNumber { get; set; }
        public string EncounterId { get; set; }
        public string VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string BloodPressure { get; set; }
        public string Temperature { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? BMI { get; set; }
        public string STIScreening { get; set; }
        public string STISymptoms { get; set; }
        public string STITreated { get; set; }
        public string Circumcised { get; set; }
        public string VMMCReferral { get; set; }
        public DateTime? LMP { get; set; }
        public string MenopausalStatus { get; set; }
        public string PregnantAtThisVisit { get; set; }
        public DateTime? EDD { get; set; }
        public string PlanningToGetPregnant { get; set; }
        public string PregnancyPlanned { get; set; }
        public string PregnancyEnded { get; set; }
        public DateTime? PregnancyEndDate { get; set; }
        public string PregnancyOutcome { get; set; }
        public string BirthDefects { get; set; }
        public string Breastfeeding { get; set; }
        public string FamilyPlanningStatus { get; set; }
        public string FPMethods { get; set; }
        public string AdherenceDone { get; set; }
        public string AdherenceOutcome { get; set; }
        public string AdherenceReasons { get; set; }
        public string SymptomsAcuteHIV { get; set; }
        public string ContraindicationsPrep { get; set; }
        public string PrepTreatmentPlan { get; set; }
        public string PrepPrescribed { get; set; }
        public string RegimenPrescribed { get; set; }
        public string MonthsPrescribed { get; set; }
        public string CondomsIssued { get; set; }
        public string Tobegivennextappointment { get; set; }
        public string Reasonfornotgivingnextappointment { get; set; }
        public string HepatitisBPositiveResult { get; set; }
        public string HepatitisCPositiveResult { get; set; }
        public string VaccinationForHepBStarted { get; set; }
        public string TreatedForHepB { get; set; }
        public string VaccinationForHepCStarted { get; set; }
        public string TreatedForHepC { get; set; }
        public DateTime? NextAppointment { get; set; }
        public string ClinicalNotes { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
