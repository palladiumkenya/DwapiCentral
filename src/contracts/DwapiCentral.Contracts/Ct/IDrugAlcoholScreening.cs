using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
     public interface IDrugAlcoholScreening 
    {
            Guid Id { get; set; }
            ulong Mhash { get; set; }
            int? VisitID { get; set; }
            DateTime? VisitDate { get; set; }
            string? FacilityName { get; set; }            
            string? DrinkingAlcohol { get; set; }
            string? Smoking { get; set; }
            string? DrugUse { get; set; }

            int? PatientPk { get; set; }
            int SiteCode { get; set; }
            string RecordUUID { get; set; }
            DateTime? Date_Created { get; set; }
            DateTime? Date_Last_Modified { get; set; }

            DateTime? DateLastModified { get; set; }
            DateTime? DateExtracted { get; set; }
            DateTime? Created { get; set; }
            DateTime? Updated { get; set; }
            bool? Voided { get; set; }
    


     }
}
