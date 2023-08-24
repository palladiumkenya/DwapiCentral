using DwapiCentral.Contracts.Prep;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models
{
    public class PrepPharmacy : IPrepPharmacy
    {

        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string PrepNumber { get; set; }
        public string? FacilityName { get; set; }
        public string? HtsNumber { get; set; }
        public int? VisitID { get; set; }
        public string? RegimenPrescribed { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }       
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
