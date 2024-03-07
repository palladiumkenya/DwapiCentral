using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class PatientIptSourceDto : IIpt
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? OnTBDrugs { get; set; }
        public string? OnIPT { get; set; }
        public string? EverOnIPT { get; set; }
        public string? Cough { get; set; }
        public string? Fever { get; set; }
        public string? NoticeableWeightLoss { get; set; }
        public string? NightSweats { get; set; }
        public string? Lethargy { get; set; }
        public string? ICFActionTaken { get; set; }
        public string? TestResult { get; set; }
        public string? TBClinicalDiagnosis { get; set; }
        public string? ContactsInvited { get; set; }
        public string? EvaluatedForIPT { get; set; }
        public string? StartAntiTBs { get; set; }
        public DateTime? TBRxStartDate { get; set; }
        public string? TBScreening { get; set; }
        public string? IPTClientWorkUp { get; set; }
        public string? StartIPT { get; set; }
        public string? IndicationForIPT { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public PatientIptSourceDto()
        {
        }

        public PatientIptSourceDto(IptExtract IptExtract)
        {
            FacilityName = IptExtract.FacilityName;
            VisitID = IptExtract.VisitID;
            VisitDate = IptExtract.VisitDate;
            OnTBDrugs = IptExtract.OnTBDrugs;
            OnIPT = IptExtract.OnIPT;
            EverOnIPT = IptExtract.EverOnIPT;
            Cough = IptExtract.Cough;
            Fever = IptExtract.Fever;
            NoticeableWeightLoss = IptExtract.NoticeableWeightLoss;
            NightSweats = IptExtract.NightSweats;
            Lethargy = IptExtract.Lethargy;
            ICFActionTaken = IptExtract.ICFActionTaken;
            TestResult = IptExtract.TestResult;
            TBClinicalDiagnosis = IptExtract.TBClinicalDiagnosis;
            ContactsInvited = IptExtract.ContactsInvited;
            EvaluatedForIPT = IptExtract.EvaluatedForIPT;
            StartAntiTBs = IptExtract.StartAntiTBs;
            TBRxStartDate = IptExtract.TBRxStartDate;
            TBScreening = IptExtract.TBScreening;
            IPTClientWorkUp = IptExtract.IPTClientWorkUp;
            StartIPT = IptExtract.StartIPT;
            IndicationForIPT = IptExtract.IndicationForIPT;
        

            SiteCode = IptExtract.SiteCode;
            PatientPk = IptExtract.PatientPk;
          
            Date_Created = IptExtract.Date_Created;
            Date_Last_Modified = IptExtract.Date_Last_Modified;
            RecordUUID = IptExtract.RecordUUID;

        }



        public IEnumerable<PatientIptSourceDto> GenerateIptExtractDtOs(IEnumerable<IptExtract> extracts)
        {
            var statusExtractDtos = new List<PatientIptSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new PatientIptSourceDto(e));
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
