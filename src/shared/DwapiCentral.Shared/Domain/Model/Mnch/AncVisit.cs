using DwapiCentral.Contracts.Common;
using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Mnch
{
    public class AncVisit : Entity<Guid>
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
        public string PatientMnchID { get; set; }
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string ANCClinicNumber { get; set; }
        public int? ANCVisitNo { get; set; }
        public int? GestationWeeks { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Temp { get; set; }
        public int? PulseRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public decimal? OxygenSaturation { get; set; }
        public int? MUAC { get; set; }
        public int? BP { get; set; }
        public string BreastExam { get; set; }
        public string AntenatalExercises { get; set; }
        public string FGM { get; set; }
        public string FGMComplications { get; set; }
        public decimal? Haemoglobin { get; set; }
        public string DiabetesTest { get; set; }
        public string TBScreening { get; set; }
        public string CACxScreen { get; set; }
        public string CACxScreenMethod { get; set; }
        public int? WHOStaging { get; set; }
        public string VLSampleTaken { get; set; }
        public DateTime? VLDate { get; set; }
        public string VLResult { get; set; }
        public string SyphilisTreatment { get; set; }
        public string HIVStatusBeforeANC { get; set; }
        public string HIVTestingDone { get; set; }
        public string HIVTestType { get; set; }
        public string HIVTest1 { get; set; }
        public string HIVTest1Result { get; set; }
        public string HIVTest2 { get; set; }
        public string HIVTest2Result { get; set; }
        public string HIVTestFinalResult { get; set; }
        public string SyphilisTestDone { get; set; }
        public string SyphilisTestType { get; set; }
        public string SyphilisTestResults { get; set; }
        public string SyphilisTreated { get; set; }
        public string MotherProphylaxisGiven { get; set; }
        public DateTime? MotherGivenHAART { get; set; }
        public string AZTBabyDispense { get; set; }
        public string NVPBabyDispense { get; set; }
        public string ChronicIllness { get; set; }
        public string CounselledOn { get; set; }
        public string PartnerHIVTestingANC { get; set; }
        public string PartnerHIVStatusANC { get; set; }
        public string PostParturmFP { get; set; }
        public string Deworming { get; set; }
        public string MalariaProphylaxis { get; set; }
        public string TetanusDose { get; set; }
        public string IronSupplementsGiven { get; set; }
        public string ReceivedMosquitoNet { get; set; }
        public string PreventiveServices { get; set; }
        public string UrinalysisVariables { get; set; }
        public string ReferredFrom { get; set; }
        public string ReferredTo { get; set; }
        public string ReferralReasons { get; set; }
        public DateTime? NextAppointmentANC { get; set; }
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
