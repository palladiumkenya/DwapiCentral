using DwapiCentral.Contracts.Mnch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model
{
    public class MotherBabyPair : IMotherBabyPair
    {
        [Key]
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public int BabyPatientPK { get; set; }
        public int MotherPatientPK { get; set; }
        public string? BabyPatientMncHeiID { get; set; }
        public string? MotherPatientMncHeiID { get; set; }
        public string? PatientIDCCC { get; set; }       
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
