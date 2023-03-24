using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IDrugAlcoholScreening : IEntity
    {
          string FacilityName { get; set; }
          int? VisitID { get; set; }
          DateTime? VisitDate { get; set; }
          string DrinkingAlcohol { get; set; }
          string Smoking { get; set; }
          string DrugUse { get; set; }
          Guid PatientId { get; set; }
    }
}
