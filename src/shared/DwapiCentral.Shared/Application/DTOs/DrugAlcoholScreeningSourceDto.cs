using System;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using PalladiumDwh.Shared.Interfaces;

namespace DwapiCentral.Shared.Application.DTOs
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
    }
}