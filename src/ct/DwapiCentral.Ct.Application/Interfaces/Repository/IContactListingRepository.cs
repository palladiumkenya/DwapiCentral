using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
{

    public interface IContactListingRepository : IRepository<ContactListingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<ContactListingExtract> profileContactListingExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<ContactListingExtract> extracts);

        void SyncNew(List<ContactListingProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<ContactListingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
