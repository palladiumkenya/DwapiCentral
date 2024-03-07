using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class DepressionScreeningSourceDto : IDepressionScreening
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? PHQ9_1 { get; set; }
        public string? PHQ9_2 { get; set; }
        public string? PHQ9_3 { get; set; }
        public string? PHQ9_4 { get; set; }
        public string? PHQ9_5 { get; set; }
        public string? PHQ9_6 { get; set; }
        public string? PHQ9_7 { get; set; }
        public string? PHQ9_8 { get; set; }
        public string? PHQ9_9 { get; set; }
        public string? PHQ_9_rating { get; set; }
        public int? DepressionAssesmentScore { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public DepressionScreeningSourceDto()
        {
        }

        public DepressionScreeningSourceDto(DepressionScreeningExtract DepressionScreeningExtract)
        {
            FacilityName = DepressionScreeningExtract.FacilityName;
            VisitID = DepressionScreeningExtract.VisitID;
            VisitDate = DepressionScreeningExtract.VisitDate;
            PHQ9_1 = DepressionScreeningExtract.PHQ9_1;
            PHQ9_2 = DepressionScreeningExtract.PHQ9_2;
            PHQ9_3 = DepressionScreeningExtract.PHQ9_3;
            PHQ9_4 = DepressionScreeningExtract.PHQ9_4;
            PHQ9_5 = DepressionScreeningExtract.PHQ9_5;
            PHQ9_6 = DepressionScreeningExtract.PHQ9_6;
            PHQ9_7 = DepressionScreeningExtract.PHQ9_7;
            PHQ9_8 = DepressionScreeningExtract.PHQ9_8;
            PHQ9_9 = DepressionScreeningExtract.PHQ9_9;
            PHQ_9_rating = DepressionScreeningExtract.PHQ_9_rating;
            DepressionAssesmentScore = DepressionScreeningExtract.DepressionAssesmentScore;

            SiteCode = DepressionScreeningExtract.SiteCode;
            PatientPk = DepressionScreeningExtract.PatientPk;
           
            Date_Created = DepressionScreeningExtract.Date_Created;
            Date_Last_Modified = DepressionScreeningExtract.Date_Last_Modified;
            RecordUUID = DepressionScreeningExtract.RecordUUID;

        }



        public IEnumerable<DepressionScreeningSourceDto> GenerateDepressionScreeningExtractDtOs(IEnumerable<DepressionScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<DepressionScreeningSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DepressionScreeningSourceDto(e));
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