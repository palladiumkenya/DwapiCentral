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
    public class MnchEnrolment : Entity<Guid>
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
        public string PatientMnchID { get; set; }
        public string FacilityName { get; set; }
        public string ServiceType { get; set; }
        public DateTime? EnrollmentDateAtMnch { get; set; }
        public DateTime? MnchNumber { get; set; }
        public DateTime? FirstVisitAnc { get; set; }
        public string Parity { get; set; }
        public int Gravidae { get; set; }
        public DateTime? LMP { get; set; }
        public DateTime? EDDFromLMP { get; set; }
        public string HIVStatusBeforeANC { get; set; }
        public DateTime? HIVTestDate { get; set; }
        public string PartnerHIVStatus { get; set; }
        public DateTime? PartnerHIVTestDate { get; set; }
        public string BloodGroup { get; set; }
        public string StatusAtMnch { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
