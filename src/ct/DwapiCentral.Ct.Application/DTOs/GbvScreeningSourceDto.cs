using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class GbvScreeningSourceDto : IGbvScreening
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? IPV { get; set; }
        public string? PhysicalIPV { get; set; }
        public string? EmotionalIPV { get; set; }
        public string? SexualIPV { get; set; }
        public string? IPVRelationship { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public GbvScreeningSourceDto()
        {
        }

        public GbvScreeningSourceDto(GbvScreeningExtract GbvScreeningExtract)
        {
            FacilityName = GbvScreeningExtract.FacilityName;
            VisitID = GbvScreeningExtract.VisitID;
            VisitDate = GbvScreeningExtract.VisitDate;
            IPV = GbvScreeningExtract.IPV;
            PhysicalIPV = GbvScreeningExtract.PhysicalIPV;
            EmotionalIPV = GbvScreeningExtract.EmotionalIPV;
            SexualIPV = GbvScreeningExtract.SexualIPV;
            IPVRelationship = GbvScreeningExtract.IPVRelationship;

            SiteCode = GbvScreeningExtract.SiteCode;
            PatientPk = GbvScreeningExtract.PatientPk;
           
            Date_Created = GbvScreeningExtract.Date_Created;
            Date_Last_Modified = GbvScreeningExtract.Date_Last_Modified;
            RecordUUID = GbvScreeningExtract.RecordUUID;

        }



        public IEnumerable<GbvScreeningSourceDto> GenerateGbvScreeningExtractDtOs(IEnumerable<GbvScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<GbvScreeningSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new GbvScreeningSourceDto(e));
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
