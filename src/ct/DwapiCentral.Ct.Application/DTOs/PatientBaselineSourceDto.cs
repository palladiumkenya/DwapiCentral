using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class PatientBaselineSourceDto : IPatientBaselines
    {
        public Guid Id { get ; set ; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? bCD4 { get ; set ; }
        public DateTime? bCD4Date { get ; set ; }
        public int? bWAB { get ; set ; }
        public DateTime? bWABDate { get ; set ; }
        public int? bWHO { get ; set ; }
        public DateTime? bWHODate { get ; set ; }
        public int? eWAB { get ; set ; }
        public DateTime? eWABDate { get ; set ; }
        public int? eCD4 { get ; set ; }
        public DateTime? eCD4Date { get ; set ; }
        public int? eWHO { get ; set ; }
        public DateTime? eWHODate { get ; set ; }
        public int? lastWHO { get ; set ; }
        public DateTime? lastWHODate { get ; set ; }
        public int? lastCD4 { get ; set ; }
        public DateTime? lastCD4Date { get ; set ; }
        public int? lastWAB { get ; set ; }
        public DateTime? lastWABDate { get ; set ; }
        public int? m12CD4 { get ; set ; }
        public DateTime? m12CD4Date { get ; set ; }
        public int? m6CD4 { get ; set ; }
        public DateTime? m6CD4Date { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public PatientBaselineSourceDto()
        {

        }

        public PatientBaselineSourceDto(PatientBaselinesExtract patientBaselinesExtract)
        {

            bCD4 = patientBaselinesExtract.bCD4;
            bCD4Date = patientBaselinesExtract.bCD4Date;
            bWAB = patientBaselinesExtract.bWAB;
            bWABDate = patientBaselinesExtract.bWABDate;
            bWHO = patientBaselinesExtract.bWHO;
            bWHODate = patientBaselinesExtract.bWHODate;
            eWAB = patientBaselinesExtract.eWAB;
            eWABDate = patientBaselinesExtract.eWABDate;
            eCD4 = patientBaselinesExtract.eCD4;
            eCD4Date = patientBaselinesExtract.eCD4Date;
            eWHO = patientBaselinesExtract.eWHO;
            eWHODate = patientBaselinesExtract.eWHODate;
            lastWHO = patientBaselinesExtract.lastWHO;
            lastWHODate = patientBaselinesExtract.lastWHODate;
            lastCD4 = patientBaselinesExtract.lastCD4;
            lastCD4Date = patientBaselinesExtract.lastCD4Date;
            lastWAB = patientBaselinesExtract.lastWAB;
            lastWABDate = patientBaselinesExtract.lastWABDate;
            m12CD4 = patientBaselinesExtract.m12CD4;
            m12CD4Date = patientBaselinesExtract.m12CD4Date;
            m6CD4 = patientBaselinesExtract.m6CD4;
            m6CD4Date = patientBaselinesExtract.m6CD4Date;
            SiteCode = patientBaselinesExtract.SiteCode;
            PatientPk = patientBaselinesExtract.PatientPk;
            
            Date_Created = patientBaselinesExtract.Date_Created;
            Date_Last_Modified = patientBaselinesExtract.Date_Last_Modified;
            RecordUUID = patientBaselinesExtract.RecordUUID;

        }



        public IEnumerable<PatientBaselineSourceDto> GeneratePatientBaselinesExtractDtOs(IEnumerable<PatientBaselinesExtract> extracts)
        {
            var baselinesExtractDtos = new List<PatientBaselineSourceDto>();
            foreach (var e in extracts.ToList())
            {
                baselinesExtractDtos.Add(new PatientBaselineSourceDto(e));
            }
            return baselinesExtractDtos;
        }

        public virtual  bool IsValid()
        {
            return SiteCode > 0 && 
                PatientPk > 0;
        }
    }
}
