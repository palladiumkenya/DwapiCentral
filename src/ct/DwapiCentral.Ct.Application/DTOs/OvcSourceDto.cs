using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class OvcSourceDto : IOvc
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? OVCEnrollmentDate { get; set; }
        public string? RelationshipToClient { get; set; }
        public string? EnrolledinCPIMS { get; set; }
        public string? CPIMSUniqueIdentifier { get; set; }
        public string? PartnerOfferingOVCServices { get; set; }
        public string? OVCExitReason { get; set; }
        public DateTime? ExitDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public OvcSourceDto()
        {
        }

        public OvcSourceDto(OvcExtract OvcExtract)
        {
            FacilityName = OvcExtract.FacilityName;
            VisitID = OvcExtract.VisitID;
            VisitDate = OvcExtract.VisitDate;
            OVCEnrollmentDate = OvcExtract.OVCEnrollmentDate;
            RelationshipToClient = OvcExtract.RelationshipToClient;
            EnrolledinCPIMS = OvcExtract.EnrolledinCPIMS;
            CPIMSUniqueIdentifier = OvcExtract.CPIMSUniqueIdentifier;
            PartnerOfferingOVCServices = OvcExtract.PartnerOfferingOVCServices;
            OVCExitReason = OvcExtract.OVCExitReason;
            ExitDate = OvcExtract.ExitDate;

            PatientPk = OvcExtract.PatientPk;
            SiteCode = OvcExtract.SiteCode;
          
            Date_Created = OvcExtract.Date_Created;
            Date_Last_Modified = OvcExtract.Date_Last_Modified;
            RecordUUID = OvcExtract.RecordUUID;

        }

        public IEnumerable<OvcSourceDto> GenerateOvcExtractDtOs(IEnumerable<OvcExtract> extracts)
        {
            var statusExtractDtos = new List<OvcSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new OvcSourceDto(e));
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