using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
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
