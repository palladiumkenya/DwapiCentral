using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Shared.Domain.Entities;
using DwapiCentral.Shared.Domain.Model.Ct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Mnch
{
    public class MnchArt : Entity<Guid>
    {
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool? Processed { get; set; }
        public string QueueId { get; set; }
        public string Status { get; set; }
        public DateTime? StatusDate { get; set; }
        public DateTime? DateExtracted { get; set; }
        public Guid FacilityId { get; set; }
        public string Pkv { get; set; }
        public string PatientMnchID { get; set; }
        public string PatientHeiID { get; set; }
        public string FacilityName { get; set; }
        public DateTime? RegistrationAtCCC { get; set; }
        public DateTime? StartARTDate { get; set; }
        public string StartRegimen { get; set; }
        public string StartRegimenLine { get; set; }
        public string StatusAtCCC { get; set; }
        public DateTime? LastARTDate { get; set; }
        public string LastRegimen { get; set; }
        public string LastRegimenLine { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
