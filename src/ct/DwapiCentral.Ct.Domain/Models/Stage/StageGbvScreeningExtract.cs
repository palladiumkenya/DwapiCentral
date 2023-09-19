using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageGbvScreeningExtract : StageExtract,IGbvScreening
    {
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? IPV { get; set; }
        public string? PhysicalIPV { get; set; }
        public string? EmotionalIPV { get; set; }
        public string? SexualIPV { get; set; }
        public string? IPVRelationship { get; set; }
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
