using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{

    public interface IIptRepository : IRepository<IptExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<IptExtract> profileIptExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<IptExtract> extracts);

        void SyncNew(List<IptProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<IptProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
