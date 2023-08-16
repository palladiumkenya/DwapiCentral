using DwapiCentral.Contracts.Hts;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model.Stage
{
    public class StageHtsPartnerNotificationServices : IHtsPartnerNotificationServices
    {
        
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string HtsNumber { get ; set ; }
        public int? PartnerPersonID { get ; set ; }
        public DateTime? DateElicited { get ; set ; }
        public string FacilityName { get ; set ; }
        public int? PartnerPatientPk { get ; set ; }
        public int? Age { get ; set ; }
        public string? Sex { get ; set ; }
        public string? RelationsipToIndexClient { get ; set ; }
        public string? ScreenedForIpv { get ; set ; }
        public string? IpvScreeningOutcome { get ; set ; }
        public string? CurrentlyLivingWithIndexClient { get ; set ; }
        public string? KnowledgeOfHivStatus { get ; set ; }
        public string? PnsApproach { get ; set ; }
        public string? PnsConsent { get ; set ; }
        public string? LinkedToCare { get ; set ; }
        public DateTime? LinkDateLinkedToCare { get ; set ; }
        public string? CccNumber { get ; set ; }
        public string? FacilityLinkedTo { get ; set ; }
        public DateTime? Dob { get ; set ; }
        public string? MaritalStatus { get ; set ; }
        public DateTime? Date_Last_Modified { get ; set ; }
        public DateTime? Date_Created { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public Guid? ManifestId { get; set; }
        public LiveStage LiveStage { get; set; }
    }
}
