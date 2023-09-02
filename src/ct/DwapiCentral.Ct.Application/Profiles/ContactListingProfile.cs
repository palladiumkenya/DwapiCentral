using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DwapiCentral.Ct.Application.Profiles
{
    public class ContactListingProfile : ExtractProfile<ContactListingExtract>, IContactListingProfile
    {
        public List<ContactListingSourceDto> ContactListingExtracts { get; set; } = new List<ContactListingSourceDto>();

        public static ContactListingProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new ContactListingProfile
            {
                Facility = new FacilityDTO(facility),
                //////Demographic = new PatientExtractDTO(patient),
                ContactListingExtracts =
                    new ContactListingSourceDto().GenerateContactListingExtractDtOs(patient.ContactListingExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<ContactListingProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<ContactListingProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && ContactListingExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != ContactListingExtracts;
        }

       
    }
}
