using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageContactListingExtract : StageExtract,  IContactListing
    {
        public string? FacilityName { get ; set ; }
        public int? PartnerPersonID { get ; set ; }
        public string? ContactAge { get ; set ; }
        public string? ContactSex { get ; set ; }
        public string? ContactMaritalStatus { get ; set ; }
        public string? RelationshipWithPatient { get ; set ; }
        public string? ScreenedForIpv { get ; set ; }
        public string? IpvScreening { get ; set ; }
        public string? IPVScreeningOutcome { get ; set ; }
        public string? CurrentlyLivingWithIndexClient { get ; set ; }
        public string? KnowledgeOfHivStatus { get ; set ; }
        public string? PnsApproach { get ; set ; }
        public int? ContactPatientPK { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? DateCreated { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }

  
}
