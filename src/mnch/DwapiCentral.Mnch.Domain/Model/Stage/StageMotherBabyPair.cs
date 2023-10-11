using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model.Stage
{
    public class StageMotherBabyPair : IMotherBabyPair
    {
        public Guid Id { get; set; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public string RecordUUID { get ; set ; }
        public int BabyPatientPK { get ; set ; }
        public int MotherPatientPK { get ; set ; }
        public string? BabyPatientMncHeiID { get ; set ; }
        public string? MotherPatientMncHeiID { get ; set ; }
        public string? PatientIDCCC { get ; set ; }
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
