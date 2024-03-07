using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class CervicalCancerScreeningSourceDto :  ICervicalCancerScreening
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }        
        public string? VisitType { get; set; }
        public string? ScreeningMethod { get; set; }
        public string? TreatmentToday { get; set; }
        public string? ReferredOut { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public string? ScreeningType { get; set; }
        public string? ScreeningResult { get; set; }
        public string? PostTreatmentComplicationCause { get; set; }
        public string? OtherPostTreatmentComplication { get; set; }
        public string? ReferralReason { get; set; }        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }


        public CervicalCancerScreeningSourceDto()
        {
        }

        public CervicalCancerScreeningSourceDto(CervicalCancerScreeningExtract CervicalCancerScreeningExtract)
        {
            FacilityName = CervicalCancerScreeningExtract.FacilityName;
            VisitID = CervicalCancerScreeningExtract.VisitID;
            VisitDate = CervicalCancerScreeningExtract.VisitDate;

            VisitType = CervicalCancerScreeningExtract.VisitType;
            ScreeningMethod = CervicalCancerScreeningExtract.ScreeningMethod;
            TreatmentToday = CervicalCancerScreeningExtract.TreatmentToday;
            ReferredOut = CervicalCancerScreeningExtract.ReferredOut;
            NextAppointmentDate = CervicalCancerScreeningExtract.NextAppointmentDate;
            ScreeningType = CervicalCancerScreeningExtract.ScreeningType;
            ScreeningResult = CervicalCancerScreeningExtract.ScreeningResult;
            PostTreatmentComplicationCause = CervicalCancerScreeningExtract.PostTreatmentComplicationCause;
            OtherPostTreatmentComplication = CervicalCancerScreeningExtract.OtherPostTreatmentComplication;
            ReferralReason = CervicalCancerScreeningExtract.ReferralReason;

           
            Date_Created = CervicalCancerScreeningExtract.Date_Created;
            Date_Last_Modified = CervicalCancerScreeningExtract.Date_Last_Modified;
            RecordUUID = CervicalCancerScreeningExtract.RecordUUID;


        }

        public IEnumerable<CervicalCancerScreeningSourceDto> GenerateCervicalCancerScreeningExtractDtOs(IEnumerable<CervicalCancerScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<CervicalCancerScreeningSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new CervicalCancerScreeningSourceDto(e));
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
