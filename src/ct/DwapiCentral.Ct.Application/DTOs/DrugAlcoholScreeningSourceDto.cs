using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class DrugAlcoholScreeningSourceDto : IDrugAlcoholScreening
    {
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? DrinkingAlcohol { get; set; }
        public string? Smoking { get; set; }
        public string? DrugUse { get; set; }
        public int? PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public DrugAlcoholScreeningSourceDto()
        {
        }

        public DrugAlcoholScreeningSourceDto(DrugAlcoholScreeningExtract DrugAlcoholScreeningExtract)
        {
            FacilityName = DrugAlcoholScreeningExtract.FacilityName;
            VisitID = DrugAlcoholScreeningExtract.VisitID;
            VisitDate = DrugAlcoholScreeningExtract.VisitDate;
            DrinkingAlcohol = DrugAlcoholScreeningExtract.DrinkingAlcohol;
            Smoking = DrugAlcoholScreeningExtract.Smoking;
            DrugUse = DrugAlcoholScreeningExtract.DrugUse;

            SiteCode = DrugAlcoholScreeningExtract.SiteCode;
            PatientPk = DrugAlcoholScreeningExtract.PatientPk;
           
            Date_Created = DrugAlcoholScreeningExtract.Date_Created;
            Date_Last_Modified = DrugAlcoholScreeningExtract.Date_Last_Modified;
            RecordUUID = DrugAlcoholScreeningExtract.RecordUUID;

        }

        public IEnumerable<DrugAlcoholScreeningSourceDto> GenerateDrugAlcoholScreeningExtractDtOs(IEnumerable<DrugAlcoholScreeningExtract> extracts)
        {
            var statusExtractDtos = new List<DrugAlcoholScreeningSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new DrugAlcoholScreeningSourceDto(e));
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