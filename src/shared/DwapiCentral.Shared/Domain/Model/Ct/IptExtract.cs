using System;
using DwapiCentral.Contracts.Ct;


namespace DwapiCentral.Shared.Domain.Model.Ct
{
    public class IptExtract 
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string OnTBDrugs { get; set; }
        public string OnIPT { get; set; }
        public string EverOnIPT { get; set; }
        public string Cough { get; set; }
        public string Fever { get; set; }
        public string NoticeableWeightLoss { get; set; }
        public string NightSweats { get; set; }
        public string Lethargy { get; set; }
        public string ICFActionTaken { get; set; }
        public string TestResult { get; set; }
        public string TBClinicalDiagnosis { get; set; }
        public string ContactsInvited { get; set; }
        public string EvaluatedForIPT { get; set; }
        public string StartAntiTBs { get; set; }
        public DateTime? TBRxStartDate { get; set; }
        public string TBScreening { get; set; }
        public string IPTClientWorkUp { get; set; }
        public string StartIPT { get; set; }
        public string IndicationForIPT { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public string Emr { get ; set ; }
        public string Project { get ; set ; }
        public bool Voided { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public DateTime? Extracted { get ; set ; }

        public IptExtract()
        {
            Created = DateTime.Now;
        }

        public IptExtract(string facilityName, int? visitId, DateTime? visitDate, string onTbDrugs, string onIpt, string everOnIpt, string cough, string fever, string noticeableWeightLoss, string nightSweats, string lethargy, string icfActionTaken, string testResult, string tbClinicalDiagnosis, string contactsInvited, string evaluatedForIpt, string startAntiTBs, DateTime? tbRxStartDate, string tbScreening, string iptClientWorkUp, string startIpt, string indicationForIpt,
            Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            OnTBDrugs = onTbDrugs;
            OnIPT = onIpt;
            EverOnIPT = everOnIpt;
            Cough = cough;
            Fever = fever;
            NoticeableWeightLoss = noticeableWeightLoss;
            NightSweats = nightSweats;
            Lethargy = lethargy;
            ICFActionTaken = icfActionTaken;
            TestResult = testResult;
            TBClinicalDiagnosis = tbClinicalDiagnosis;
            ContactsInvited = contactsInvited;
            EvaluatedForIPT = evaluatedForIpt;
            StartAntiTBs = startAntiTBs;
            TBRxStartDate = tbRxStartDate;
            TBScreening = tbScreening;
            IPTClientWorkUp = iptClientWorkUp;
            StartIPT = startIpt;
            IndicationForIPT = indicationForIpt;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}
