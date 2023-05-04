using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{

    public interface IDefaulterTracingRepository : IRepository<DefaulterTracingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> profileDefaulterTracingExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> extracts);

        void SyncNew(List<DefaulterTracingProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<DefaulterTracingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
