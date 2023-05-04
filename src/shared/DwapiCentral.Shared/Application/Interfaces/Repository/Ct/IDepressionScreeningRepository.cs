using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{

    public interface IDepressionScreeningRepository : IRepository<DepressionScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> profileDepressionScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> extracts);

        void SyncNew(List<DepressionScreeningProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<DepressionScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
