using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
    public interface ICervicalCancerScreening : IExtract
    {
         ulong Mhash { get; set; }
         Guid Id { get; set; }
         string? FacilityName { get; set; }
         int? VisitID { get; set; }
         DateTime? VisitDate { get; set; }
         string? VisitType { get; set; }
         string? ScreeningMethod { get; set; }
         string? TreatmentToday { get; set; }
         string? ReferredOut { get; set; }
         DateTime? NextAppointmentDate { get; set; }
         string? ScreeningType { get; set; }
         string? ScreeningResult { get; set; }
         string? PostTreatmentComplicationCause { get; set; }
         string? OtherPostTreatmentComplication { get; set; }
         string? ReferralReason { get; set; } 
    }

}
