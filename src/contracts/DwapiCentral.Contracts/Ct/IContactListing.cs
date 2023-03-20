using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Ct
{
    public interface IContactListing : IEntity
    {
        string FacilityName { get; set; }
        int? PartnerPersonID { get; set; }
        string ContactAge { get; set; }
        string ContactSex { get; set; }
        string ContactMaritalStatus { get; set; }
        string RelationshipWithPatient { get; set; }
        string ScreenedForIpv { get; set; }
        string IpvScreening { get; set; }
        string IPVScreeningOutcome { get; set; }
        string CurrentlyLivingWithIndexClient { get; set; }
        string KnowledgeOfHivStatus { get; set; }
        string PnsApproach { get; set; }
        int? ContactPatientPK { get; set; }

        Guid PatientId { get; set; }
    }
}
