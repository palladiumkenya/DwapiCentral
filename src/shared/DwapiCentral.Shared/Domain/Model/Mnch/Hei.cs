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
    public class Hei  : Entity<Guid>
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
        public string FacilityName { get; set; }
        public string PatientMnchID { get; set; }
        public DateTime? DNAPCR1Date { get; set; }
        public DateTime? DNAPCR2Date { get; set; }
        public DateTime? DNAPCR3Date { get; set; }
        public DateTime? ConfirmatoryPCRDate { get; set; }
        public DateTime? BasellineVLDate { get; set; }
        public DateTime? FinalyAntibodyDate { get; set; }
        public string DNAPCR1 { get; set; }
        public string DNAPCR2 { get; set; }
        public string DNAPCR3 { get; set; }
        public string ConfirmatoryPCR { get; set; }
        public string BasellineVL { get; set; }
        public string FinalyAntibody { get; set; }
        public DateTime? HEIExitDate { get; set; }
        public string HEIHIVStatus { get; set; }
        public string HEIExitCritearia { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
