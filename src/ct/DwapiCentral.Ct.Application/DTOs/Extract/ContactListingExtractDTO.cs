using DwapiCentral.Ct.Application.Interfaces.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DwapiCentral.Ct.Application.DTOs.Extract
{
    public class ContactListingExtractDTO : IContactListingExtractDTO
    {
        public string FacilityName { get; set; }
        public int? PartnerPersonID { get; set; }
        public string ContactAge { get; set; }
        public string ContactSex { get; set; }
        public string ContactMaritalStatus { get; set; }
        public string RelationshipWithPatient { get; set; }
        public string ScreenedForIpv { get; set; }
        public string IpvScreening { get; set; }
        public string IPVScreeningOutcome { get; set; }
        public string CurrentlyLivingWithIndexClient { get; set; }
        public string KnowledgeOfHivStatus { get; set; }
        public string PnsApproach { get; set; }
        public int? ContactPatientPK { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public bool Voided { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public ContactListingExtractDTO()
        {
        }

        public ContactListingExtractDTO(ContactListingExtract ContactListingExtract)
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

            Emr = ContactListingExtract.Emr;
            Project = ContactListingExtract.Project;
            PatientId = ContactListingExtract.PatientId;
            Date_Created = ContactListingExtract.Date_Created;
            Date_Last_Modified = ContactListingExtract.Date_Last_Modified;
        }

        public IEnumerable<ContactListingExtractDTO> GenerateContactListingExtractDtOs(IEnumerable<ContactListingExtract> extracts)
        {
            var statusExtractDtos = new List<ContactListingExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new ContactListingExtractDTO(e));
            }
            return statusExtractDtos;
        }

        public ContactListingExtract GenerateContactListingExtract(Guid patientId)
        {
            PatientId = patientId;
            return new ContactListingExtract(
                FacilityName,
                PartnerPersonID,
                ContactAge,
                ContactSex,
                ContactMaritalStatus,
                RelationshipWithPatient,
                ScreenedForIpv,
                IpvScreening,
                IPVScreeningOutcome,
                CurrentlyLivingWithIndexClient,
                KnowledgeOfHivStatus,
                PnsApproach,
                ContactPatientPK,
                PatientId,
                Emr,
                Project,
                Date_Created,
                Date_Last_Modified
                );
        }
    }
}
