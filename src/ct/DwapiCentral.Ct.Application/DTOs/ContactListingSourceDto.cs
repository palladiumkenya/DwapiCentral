using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class ContactListingSourceDto : IContactListing
    {
        public Guid Id { get ; set ; }
        public string RecordUUID { get; set; }
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
        public DateTime? Date_Created { get ; set ; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }

        public ContactListingSourceDto()
        {
        }

        public ContactListingSourceDto(ContactListingExtract ContactListingExtract)
        {
            FacilityName = ContactListingExtract.FacilityName;
            PartnerPersonID = ContactListingExtract.PartnerPersonID;
            ContactAge = ContactListingExtract.ContactAge;
            ContactSex = ContactListingExtract.ContactSex;
            ContactMaritalStatus = ContactListingExtract.ContactMaritalStatus;
            RelationshipWithPatient = ContactListingExtract.RelationshipWithPatient;
            ScreenedForIpv = ContactListingExtract.ScreenedForIpv;
            IpvScreening = ContactListingExtract.IpvScreening;
            IPVScreeningOutcome = ContactListingExtract.IPVScreeningOutcome;
            CurrentlyLivingWithIndexClient = ContactListingExtract.CurrentlyLivingWithIndexClient;
            KnowledgeOfHivStatus = ContactListingExtract.KnowledgeOfHivStatus;
            PnsApproach = ContactListingExtract.PnsApproach;
            ContactPatientPK = ContactListingExtract.ContactPatientPK;

           
            Date_Created = ContactListingExtract.Date_Created;
            Date_Last_Modified = ContactListingExtract.Date_Last_Modified;
            RecordUUID = ContactListingExtract.RecordUUID;

        }

        public IEnumerable<ContactListingSourceDto> GenerateContactListingExtractDtOs(IEnumerable<ContactListingExtract> extracts)
        {
            var statusExtractDtos = new List<ContactListingSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new ContactListingSourceDto(e));
            }
            return statusExtractDtos;
        }


        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
