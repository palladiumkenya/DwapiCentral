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
    public class CwcEnrolment : Entity<Guid>
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
        public string PatientIDCWC { get; set; }
        public string HEIID { get; set; }
        public string MothersPkv { get; set; }
        public DateTime? RegistrationAtCWC { get; set; }
        public DateTime? RegistrationAtHEI { get; set; }
        public int? VisitID { get; set; }
        public DateTime? Gestation { get; set; }
        public string BirthWeight { get; set; }
        public decimal? BirthLength { get; set; }
        public int? BirthOrder { get; set; }
        public string BirthType { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string ModeOfDelivery { get; set; }
        public string SpecialNeeds { get; set; }
        public string SpecialCare { get; set; }
        public string HEI { get; set; }
        public string MotherAlive { get; set; }
        public string MothersCCCNo { get; set; }
        public string TransferIn { get; set; }
        public string TransferInDate { get; set; }
        public string TransferredFrom { get; set; }
        public string HEIDate { get; set; }
        public string NVP { get; set; }
        public string BreastFeeding { get; set; }
        public string ReferredFrom { get; set; }
        public string ARTMother { get; set; }
        public string ARTRegimenMother { get; set; }
        public DateTime? ARTStartDateMother { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public override void UpdateRefId()
        {
            RefId = Id;
            Id = Guid.NewGuid();
        }
    }
}
