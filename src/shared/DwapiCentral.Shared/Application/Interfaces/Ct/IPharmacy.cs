using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IPharmacy
    {
        int? VisitID { get; set; }
        string Drug { get; set; }
        string Provider { get; set; }
        DateTime? DispenseDate { get; set; }
        decimal? Duration { get; set; }
        DateTime? ExpectedReturn { get; set; }
        string TreatmentType { get; set; }
        string RegimenLine { get; set; }
        string PeriodTaken { get; set; }
        string ProphylaxisType { get; set; }
        string RegimenChangedSwitched { get; set; }
        string RegimenChangeSwitchReason { get; set; }
        string StopRegimenReason { get; set; }
        DateTime? StopRegimenDate { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
