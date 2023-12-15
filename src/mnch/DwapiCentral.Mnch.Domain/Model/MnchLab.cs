using DwapiCentral.Contracts.Mnch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model
{
    public class MnchLab : IMnchLab
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string PatientMNCH_ID { get ; set ; }
        public string? FacilityName { get ; set ; }
        public string? SatelliteName { get ; set ; }
        public int? VisitID { get ; set ; }
        public DateTime? OrderedbyDate { get ; set ; }
        public DateTime? ReportedbyDate { get ; set ; }
        public string? TestName { get ; set ; }
        public string? TestResult { get ; set ; }
        public string? LabReason { get ; set ; }
        
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
