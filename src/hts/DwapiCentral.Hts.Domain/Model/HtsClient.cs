using DwapiCentral.Contracts.Common;
using DwapiCentral.Contracts.Hts;
using DwapiCentral.Shared.Domain.Entities;

namespace DwapiCentral.Hts.Domain.Model
{
    public class HtsClient : IHtsClients
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string HtsNumber { get; set; }
        public string? Pkv { get; set; }
        public string? Occupation { get; set; }
        public string? PriorityPopulationType { get; set; }
        public string? HtsRecencyId { get; set; }
        public string? FacilityName { get; set; }
        public string? Serial { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? EncounterId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? MaritalStatus { get; set; }        
        public string? KeyPopulationType { get; set; }
        public string? PopulationType { get; set; }
        public string? PatientDisabled { get; set; }        
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Ward { get; set; }
        public string? NUPI { get; set; }
         
           
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }



        public virtual ICollection<HtsClientLinkage> HtsClientLinkages { get; set; } = new List<HtsClientLinkage>();
        public virtual ICollection<HtsEligibilityScreening> HtsClientPartners { get; set; } = new List<HtsEligibilityScreening>();
        public virtual ICollection<HtsClientTest> HtsClientTestss { get; set; } = new List<HtsClientTest>();
        public virtual ICollection<HtsClientTracing> HtsClientTracings { get; set; } = new List<HtsClientTracing>();
        public virtual ICollection<HtsPartnerNotificationServices> HtsPartnerNotificationServicess { get; set; } = new List<HtsPartnerNotificationServices>();
        public virtual ICollection<HtsPartnerTracing> HtsPartnerTracings { get; set; } = new List<HtsPartnerTracing>();
        public virtual ICollection<HtsTestKit> HtsTestKitss { get; set; } = new List<HtsTestKit>();

    }
}
