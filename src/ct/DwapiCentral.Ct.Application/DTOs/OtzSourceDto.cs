using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class OtzSourceDto : IOtz
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public string? FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? OTZEnrollmentDate { get; set; }
        public string? TransferInStatus { get; set; }
        public string? ModulesPreviouslyCovered { get; set; }
        public string? ModulesCompletedToday { get; set; }
        public string? SupportGroupInvolvement { get; set; }
        public string? Remarks { get; set; }
        public string? TransitionAttritionReason { get; set; }
        public DateTime? OutcomeDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public OtzSourceDto()
        {
        }

        public OtzSourceDto(OtzExtract OtzExtract)
        {
            FacilityName = OtzExtract.FacilityName;
            VisitID = OtzExtract.VisitID;
            VisitDate = OtzExtract.VisitDate;
            OTZEnrollmentDate = OtzExtract.OTZEnrollmentDate;
            TransferInStatus = OtzExtract.TransferInStatus;
            ModulesPreviouslyCovered = OtzExtract.ModulesPreviouslyCovered;
            ModulesCompletedToday = OtzExtract.ModulesCompletedToday;
            SupportGroupInvolvement = OtzExtract.SupportGroupInvolvement;
            Remarks = OtzExtract.Remarks;
            TransitionAttritionReason = OtzExtract.TransitionAttritionReason;
            OutcomeDate = OtzExtract.OutcomeDate;

            PatientPk = OtzExtract.PatientPk;
            SiteCode = OtzExtract.SiteCode;
         
            Date_Created = OtzExtract.Date_Created;
            Date_Last_Modified = OtzExtract.Date_Last_Modified;
            RecordUUID = OtzExtract.RecordUUID;

        }



        public IEnumerable<OtzSourceDto> GenerateOtzExtractDtOs(IEnumerable<OtzExtract> extracts)
        {
            var statusExtractDtos = new List<OtzSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OtzSourceDto(e));
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