using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model.Stage
{
    public class StageMnchArt : IMnchArt
    {
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string? Pkv { get; set; }
        public string PatientMnchID { get; set; }
        public string PatientHeiID { get; set; }
        public string? FacilityName { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? StartARTDate { get; set; }
        public string? StartRegimen { get; set; }
        public string? StartRegimenLine { get; set; }
        public string? StatusAtCCC { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string? LastRegimen { get; set; }
        public string? LastRegimenLine { get; set; }
        public string? FacilityReceivingARTCare { get; set; }
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }
    }
}
