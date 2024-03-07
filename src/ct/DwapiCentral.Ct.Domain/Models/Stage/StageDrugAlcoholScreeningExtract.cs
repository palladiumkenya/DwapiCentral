using System;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageDrugAlcoholScreeningExtract : StageExtract, IDrugAlcoholScreening
    {
        public ulong Mhash { get; set; }
        public int? VisitID { get ; set ; }
        public DateTime? VisitDate { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string? DrinkingAlcohol { get ; set ; }
        public string? Smoking { get ; set ; }
        public string? DrugUse { get ; set ; }
        public int? PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }

        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
