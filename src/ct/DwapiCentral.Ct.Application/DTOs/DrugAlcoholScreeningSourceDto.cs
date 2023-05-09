using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class DrugAlcoholScreeningSourceDto : SourceDto, IDrugAlcoholScreening
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int PatientPk { get; set; }
        public bool Voided { get; set; }
        public DateTime? Extracted { get; set; }
        Guid IDrugAlcoholScreening.PatientId { get; set; }
    }
}