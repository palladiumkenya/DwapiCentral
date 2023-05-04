using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{

    public interface IEnhancedAdherenceCounsellingRepository : IRepository<EnhancedAdherenceCounsellingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<EnhancedAdherenceCounsellingExtract> profileEnhancedAdherenceCounsellingExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<EnhancedAdherenceCounsellingExtract> extracts);

        void SyncNew(List<EnhancedAdherenceCounsellingProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<EnhancedAdherenceCounsellingProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
