using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IDrugAlcoholScreening : IExtract
    {
            Guid Id { get; set; }
            int? VisitID { get; set; }
            DateTime? VisitDate { get; set; }
            string? FacilityName { get; set; }            
            string? DrinkingAlcohol { get; set; }
            string? Smoking { get; set; }
            string? DrugUse { get; set; }
           
   
    }
}
