using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{

    public interface IGbvScreeningRepository : IRepository<GbvScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<GbvScreeningExtract> profileGbvScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<GbvScreeningExtract> extracts);

        void SyncNew(List<GbvScreeningProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<GbvScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
