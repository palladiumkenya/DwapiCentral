using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
     public interface IPrepPharmacy : IExtract
    {
            string PrepNumber { get; set; }
            string? FacilityName { get; set; }          
            string? HtsNumber { get; set; }
            int? VisitID { get; set; }
            string? RegimenPrescribed { get; set; }
            DateTime? DispenseDate { get; set; }
            decimal? Duration { get; set; }

 
    }
}
