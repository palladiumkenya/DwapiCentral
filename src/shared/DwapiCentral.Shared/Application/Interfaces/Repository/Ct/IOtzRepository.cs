using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{

    public interface IOtzRepository : IRepository<OtzExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<OtzExtract> profileOtzExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<OtzExtract> extracts);

        void SyncNew(List<OtzProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<OtzProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
