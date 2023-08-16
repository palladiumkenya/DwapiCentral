using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageIITRiskScore : StageExtract, IIITRiskScores
    {
        public string FacilityName { get; set; }
        public string SourceSysUUID { get; set; }
        public decimal? RiskScore { get; set; }
        public string? RiskFactors { get; set; }
        public string? RiskDescription { get; set; }
        public DateTime? RiskEvaluationDate { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
