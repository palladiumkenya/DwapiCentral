using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;
using System;


namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class ContactListingExtract : Entity, IContactListing
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

        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public ContactListingExtract()
        {
            Created = DateTime.Now;
        }

        public ContactListingExtract(string facilityName, int? partnerPersonId, string contactAge, string contactSex, string contactMaritalStatus, string relationshipWithPatient, string screenedForIpv, string ipvScreening, string ipvScreeningOutcome, string currentlyLivingWithIndexClient, string knowledgeOfHivStatus, string pnsApproach, int? contactPatientPK,
            Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            PartnerPersonID = partnerPersonId;
            ContactAge = contactAge;
            ContactSex = contactSex;
            ContactMaritalStatus = contactMaritalStatus;
            RelationshipWithPatient = relationshipWithPatient;
            ScreenedForIpv = screenedForIpv;
            IpvScreening = ipvScreening;
            IPVScreeningOutcome = ipvScreeningOutcome;
            CurrentlyLivingWithIndexClient = currentlyLivingWithIndexClient;
            KnowledgeOfHivStatus = knowledgeOfHivStatus;
            PnsApproach = pnsApproach;
            ContactPatientPK = contactPatientPK;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}
