using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{

    public interface IDrugAlcoholScreeningRepository : IRepository<DrugAlcoholScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DrugAlcoholScreeningExtract> profileDrugAlcoholScreeningExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<DrugAlcoholScreeningExtract> extracts);

        void SyncNew(List<DrugAlcoholScreeningProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<DrugAlcoholScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
