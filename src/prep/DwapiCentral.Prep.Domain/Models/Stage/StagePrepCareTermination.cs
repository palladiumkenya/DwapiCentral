using DwapiCentral.Contracts.Prep;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models.Stage
{
    public class StagePrepCareTermination : IPrepCareTermination
    {
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string PrepNumber { get; set; }
        public string? FacilityName { get; set; }
        public string? HtsNumber { get; set; }
        public DateTime? ExitDate { get; set; }
        public string? ExitReason { get; set; }
        public DateTime? DateOfLastPrepDose { get; set; }        
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
    }
}
