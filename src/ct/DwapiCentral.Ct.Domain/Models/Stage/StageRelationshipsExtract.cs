using DwapiCentral.Contracts.Ct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageRelationshipsExtract : StageExtract,IRelationship
    {
        
        public string? FacilityName { get; set; }
        public string? RelationshipToPatient { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
