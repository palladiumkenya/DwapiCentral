using DwapiCentral.Contracts.Hts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model
{
    public class HtsPartnerTracing : IHtsPartnerTracing
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string HtsNumber { get; set; }
        public int? PartnerPersonId { get; set; }
        public DateTime? TraceDate { get; set; }
        public string FacilityName { get; set; }
        public string? TraceType { get; set; }
        public string? TraceOutcome { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
