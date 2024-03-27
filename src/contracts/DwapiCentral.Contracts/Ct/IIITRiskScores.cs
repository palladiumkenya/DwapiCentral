using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
    public interface IIITRiskScores : IExtract
    {
        ulong Mhash { get; set; }
        string FacilityName { get; set; }
        string SourceSysUUID { get; set; }
        string? RiskScore { get; set; }
        string? RiskFactors { get; set; }
        string? RiskDescription { get; set; }
        DateTime? RiskEvaluationDate { get; set; }       
        DateTime? Date_Last_Modified { get; set; }
    }
}
